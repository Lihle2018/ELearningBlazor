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
[Authorize]
public class EnrollmentsController : ControllerBase
{
    private readonly ELearningDbContext _context;

    public EnrollmentsController(ELearningDbContext context)
    {
        _context = context;
    }

    [HttpGet("my-courses")]
    public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetMyEnrollments()
    {
        var studentId = GetCurrentStudentId();
        if (!studentId.HasValue)
        {
            return Unauthorized();
        }

        var enrollments = await _context.Enrollments
            .Include(e => e.Course)
            .Where(e => e.StudentId == studentId && e.IsActive)
            .OrderByDescending(e => e.LastAccessed ?? e.EnrollmentDate)
            .ToListAsync();

        var enrollmentDtos = enrollments.Select(e => new EnrollmentDto
        {
            Id = e.Id,
            CourseId = e.CourseId,
            EnrollmentDate = e.EnrollmentDate,
            Progress = e.Progress,
            LastAccessed = e.LastAccessed,
            IsActive = e.IsActive,
            CompletedModules = e.CompletedModules,
            Course = new CourseListDto
            {
                Id = e.Course.Id,
                Heading = e.Course.Heading,
                Name = e.Course.Name,
                ImgSrc = e.Course.ImgSrc,
                Students = e.Course.Students,
                Classes = e.Course.Classes,
                Price = e.Course.Price,
                Rating = e.Course.Rating,
                Description = e.Course.Description,
                Duration = e.Course.Duration,
                Level = e.Course.Level,
                IsEnrolled = true,
                Progress = e.Progress
            }
        });

        return Ok(enrollmentDtos);
    }

    [HttpPost("enroll")]
    public async Task<ActionResult> EnrollInCourse([FromBody] EnrollmentRequest request)
    {
        var studentId = GetCurrentStudentId();
        if (!studentId.HasValue)
        {
            return Unauthorized();
        }

        // Check if course exists
        var course = await _context.Courses.FindAsync(request.CourseId);
        if (course == null)
        {
            return NotFound("Course not found");
        }

        // Check if already enrolled
        var existingEnrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == request.CourseId);

        if (existingEnrollment != null)
        {
            if (existingEnrollment.IsActive)
            {
                return BadRequest("Already enrolled in this course");
            }
            else
            {
                // Reactivate enrollment
                existingEnrollment.IsActive = true;
                existingEnrollment.EnrollmentDate = DateTime.UtcNow;
                existingEnrollment.LastAccessed = DateTime.UtcNow;
            }
        }
        else
        {
            // Create new enrollment
            var enrollment = new Enrollment
            {
                StudentId = studentId.Value,
                CourseId = request.CourseId,
                EnrollmentDate = DateTime.UtcNow,
                LastAccessed = DateTime.UtcNow,
                Progress = 0,
                CompletedModules = new List<int>()
            };

            _context.Enrollments.Add(enrollment);
        }

        // Update course student count
        course.Students++;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Successfully enrolled in course" });
    }

    [HttpDelete("unenroll/{courseId}")]
    public async Task<ActionResult> UnenrollFromCourse(int courseId)
    {
        var studentId = GetCurrentStudentId();
        if (!studentId.HasValue)
        {
            return Unauthorized();
        }

        var enrollment = await _context.Enrollments
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId && e.IsActive);

        if (enrollment == null)
        {
            return NotFound("Enrollment not found");
        }

        // Deactivate enrollment
        enrollment.IsActive = false;

        // Update course student count
        if (enrollment.Course.Students > 0)
        {
            enrollment.Course.Students--;
        }

        await _context.SaveChangesAsync();
        return Ok(new { message = "Successfully unenrolled from course" });
    }

    [HttpPut("progress")]
    public async Task<ActionResult> UpdateProgress([FromBody] ProgressUpdateRequest request)
    {
        var studentId = GetCurrentStudentId();
        if (!studentId.HasValue)
        {
            return Unauthorized();
        }

        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == request.CourseId && e.IsActive);

        if (enrollment == null)
        {
            return NotFound("Enrollment not found");
        }

        enrollment.Progress = request.Progress;
        enrollment.CompletedModules = request.CompletedModules;
        enrollment.LastAccessed = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Progress updated successfully" });
    }

    private int? GetCurrentStudentId()
    {
        var studentIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(studentIdClaim, out int studentId) ? studentId : null;
    }
}