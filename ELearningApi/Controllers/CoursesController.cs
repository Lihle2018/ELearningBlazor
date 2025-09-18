using ELearningApi.Data;
using ELearningApi.Models;
using ELearningApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ELearningApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ELearningDbContext _context;

    public CoursesController(ELearningDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseListDto>>> GetCourses()
    {
        var studentId = GetCurrentStudentId();
        var courses = await _context.Courses.ToListAsync();

        var courseDtos = new List<CourseListDto>();
        foreach (var course in courses)
        {
            var enrollment = studentId.HasValue
                ? await _context.Enrollments
                    .FirstOrDefaultAsync(e => e.CourseId == course.Id && e.StudentId == studentId && e.IsActive)
                : null;

            courseDtos.Add(new CourseListDto
            {
                Id = course.Id,
                Heading = course.Heading,
                Name = course.Name,
                ImgSrc = course.ImgSrc,
                Students = course.Students,
                Classes = course.Classes,
                Price = course.Price,
                Rating = course.Rating,
                Description = course.Description,
                Duration = course.Duration,
                Level = course.Level,
                IsEnrolled = enrollment != null,
                Progress = enrollment?.Progress
            });
        }

        return Ok(courseDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        var studentId = GetCurrentStudentId();
        var enrollment = studentId.HasValue
            ? await _context.Enrollments
                .FirstOrDefaultAsync(e => e.CourseId == id && e.StudentId == studentId && e.IsActive)
            : null;

        var courseDto = new CourseDto
        {
            Id = course.Id,
            Heading = course.Heading,
            Name = course.Name,
            ImgSrc = course.ImgSrc,
            Students = course.Students,
            Classes = course.Classes,
            Price = course.Price,
            Rating = course.Rating,
            Description = course.Description,
            LongDescription = course.LongDescription,
            Duration = course.Duration,
            Level = course.Level,
            Language = course.Language,
            HasCertificate = course.HasCertificate,
            InstructorBio = course.InstructorBio,
            InstructorImage = course.InstructorImage,
            WhatYouLearn = course.WhatYouLearn,
            Requirements = course.Requirements,
            Modules = course.Modules,
            IsEnrolled = enrollment != null,
            Progress = enrollment?.Progress
        };

        return Ok(courseDto);
    }

    private int? GetCurrentStudentId()
    {
        var studentIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(studentIdClaim, out int studentId) ? studentId : null;
    }
}