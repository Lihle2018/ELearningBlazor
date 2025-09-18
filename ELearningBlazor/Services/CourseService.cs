using ELearningBlazor.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ELearningBlazor.Services;

public class CourseService : ICourseService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;
    private const string BaseUrl = "https://localhost:7299/api";

    public CourseService(HttpClient httpClient, IAuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    private void SetAuthHeader()
    {
        var token = _authService.GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<List<Course>> GetCoursesAsync()
    {
        try
        {
            SetAuthHeader();
            var courseDtos = await _httpClient.GetFromJsonAsync<List<CourseListDto>>($"{BaseUrl}/courses");
            if (courseDtos == null) return new List<Course>();

            return courseDtos.Select(dto => new Course
            {
                Id = dto.Id,
                Heading = dto.Heading,
                Name = dto.Name,
                ImgSrc = dto.ImgSrc,
                Students = dto.Students,
                Classes = dto.Classes,
                Price = dto.Price,
                Rating = dto.Rating,
                Description = dto.Description,
                Duration = dto.Duration,
                Level = dto.Level
            }).ToList();
        }
        catch (Exception)
        {
            return new List<Course>();
        }
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        try
        {
            SetAuthHeader();
            var courseDto = await _httpClient.GetFromJsonAsync<CourseDto>($"{BaseUrl}/courses/{id}");
            if (courseDto == null) return null;

            return new Course
            {
                Id = courseDto.Id,
                Heading = courseDto.Heading,
                Name = courseDto.Name,
                ImgSrc = courseDto.ImgSrc,
                Students = courseDto.Students,
                Classes = courseDto.Classes,
                Price = courseDto.Price,
                Rating = courseDto.Rating,
                Description = courseDto.Description,
                LongDescription = courseDto.LongDescription,
                Duration = courseDto.Duration,
                Level = courseDto.Level,
                Language = courseDto.Language,
                HasCertificate = courseDto.HasCertificate,
                InstructorBio = courseDto.InstructorBio,
                InstructorImage = courseDto.InstructorImage,
                WhatYouLearn = courseDto.WhatYouLearn,
                Requirements = courseDto.Requirements,
                Modules = courseDto.Modules
            };
        }
        catch (Exception)
        {
            return null;
        }
    }
}