using ELearningBlazor.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ELearningBlazor.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;
    private const string BaseUrl = "https://localhost:7299/api";

    private List<EnrolledCourse> _cachedEnrollments = new();
    private DateTime _lastCacheUpdate = DateTime.MinValue;
    private readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(5);

    public EnrollmentService(HttpClient httpClient, IAuthService authService)
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

    public async Task<List<EnrolledCourse>> GetMyEnrollmentsAsync()
    {
        try
        {
            SetAuthHeader();
            var enrollmentDtos = await _httpClient.GetFromJsonAsync<List<EnrollmentDto>>($"{BaseUrl}/enrollments/my-courses");
            if (enrollmentDtos == null) return new List<EnrolledCourse>();

            var enrollments = enrollmentDtos.Select(dto => new EnrolledCourse
            {
                Id = dto.Id,
                CourseId = dto.CourseId,
                EnrollmentDate = dto.EnrollmentDate,
                Progress = dto.Progress,
                LastAccessed = dto.LastAccessed,
                IsActive = dto.IsActive,
                CompletedModules = dto.CompletedModules,
                // Store course data in the EnrolledCourse if available
                Course = dto.Course != null ? new Course
                {
                    Id = dto.Course.Id,
                    Heading = dto.Course.Heading,
                    Name = dto.Course.Name,
                    ImgSrc = dto.Course.ImgSrc,
                    Students = dto.Course.Students,
                    Classes = dto.Course.Classes,
                    Price = dto.Course.Price,
                    Rating = dto.Course.Rating,
                    Description = dto.Course.Description,
                    Duration = dto.Course.Duration,
                    Level = dto.Course.Level
                } : null
            }).ToList();

            // Update cache
            _cachedEnrollments = enrollments;
            _lastCacheUpdate = DateTime.Now;

            return enrollments;
        }
        catch (Exception)
        {
            return new List<EnrolledCourse>();
        }
    }

    public async Task<bool> EnrollInCourseAsync(int courseId)
    {
        try
        {
            SetAuthHeader();
            var request = new EnrollmentRequest { CourseId = courseId };
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/enrollments/enroll", request);

            if (response.IsSuccessStatusCode)
            {
                // Invalidate cache
                _lastCacheUpdate = DateTime.MinValue;
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UnenrollFromCourseAsync(int courseId)
    {
        try
        {
            SetAuthHeader();
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/enrollments/unenroll/{courseId}");

            if (response.IsSuccessStatusCode)
            {
                // Invalidate cache
                _lastCacheUpdate = DateTime.MinValue;
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateProgressAsync(int courseId, double progress, List<int> completedModules)
    {
        try
        {
            SetAuthHeader();
            var request = new ProgressUpdateRequest
            {
                CourseId = courseId,
                Progress = progress,
                CompletedModules = completedModules
            };
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/enrollments/progress", request);

            if (response.IsSuccessStatusCode)
            {
                // Update local cache
                var enrollment = _cachedEnrollments.FirstOrDefault(e => e.CourseId == courseId);
                if (enrollment != null)
                {
                    enrollment.Progress = progress;
                    enrollment.CompletedModules = completedModules;
                    enrollment.LastAccessed = DateTime.UtcNow;
                }
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public EnrolledCourse? GetEnrollmentInfo(int courseId)
    {
        // Check if cache is valid
        if (DateTime.Now - _lastCacheUpdate > _cacheExpiry)
        {
            // Cache is expired, but we'll return cached data for immediate response
            // The next async call will refresh the cache
        }

        return _cachedEnrollments.FirstOrDefault(e => e.CourseId == courseId && e.IsActive);
    }

    public List<Course> GetEnrolledCourses()
    {
        var enrolledCourseIds = _cachedEnrollments
            .Where(e => e.IsActive)
            .Select(e => e.CourseId)
            .ToList();

        // This is a synchronous method, so we can't make async calls
        // Components should use the async methods instead
        return new List<Course>();
    }
}