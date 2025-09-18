using ELearningBlazor.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ELearningBlazor.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;
    private const string BaseUrl = "https://localhost:7299/api"; // Update this with your API URL

    public ApiService(HttpClient httpClient, AuthService authService)
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

    // Auth endpoints
    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/auth/login", request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                return result ?? new AuthResponse { Success = false, Message = "Failed to parse response" };
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                try
                {
                    var errorResponse = JsonSerializer.Deserialize<AuthResponse>(errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return errorResponse ?? new AuthResponse { Success = false, Message = "Login failed" };
                }
                catch
                {
                    return new AuthResponse { Success = false, Message = "Login failed" };
                }
            }
        }
        catch (Exception ex)
        {
            return new AuthResponse { Success = false, Message = $"Network error: {ex.Message}" };
        }
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/auth/register", request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                return result ?? new AuthResponse { Success = false, Message = "Failed to parse response" };
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                try
                {
                    var errorResponse = JsonSerializer.Deserialize<AuthResponse>(errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return errorResponse ?? new AuthResponse { Success = false, Message = "Registration failed" };
                }
                catch
                {
                    return new AuthResponse { Success = false, Message = "Registration failed" };
                }
            }
        }
        catch (Exception ex)
        {
            return new AuthResponse { Success = false, Message = $"Network error: {ex.Message}" };
        }
    }

    // Course endpoints
    public async Task<List<CourseListDto>> GetCoursesAsync()
    {
        try
        {
            SetAuthHeader();
            var response = await _httpClient.GetFromJsonAsync<List<CourseListDto>>($"{BaseUrl}/courses");
            return response ?? new List<CourseListDto>();
        }
        catch (Exception)
        {
            return new List<CourseListDto>();
        }
    }

    public async Task<CourseDto?> GetCourseAsync(int id)
    {
        try
        {
            SetAuthHeader();
            var response = await _httpClient.GetFromJsonAsync<CourseDto>($"{BaseUrl}/courses/{id}");
            return response;
        }
        catch (Exception)
        {
            return null;
        }
    }

    // Enrollment endpoints
    public async Task<List<EnrollmentDto>> GetMyEnrollmentsAsync()
    {
        try
        {
            SetAuthHeader();
            var response = await _httpClient.GetFromJsonAsync<List<EnrollmentDto>>($"{BaseUrl}/enrollments/my-courses");
            return response ?? new List<EnrollmentDto>();
        }
        catch (Exception)
        {
            return new List<EnrollmentDto>();
        }
    }

    public async Task<bool> EnrollInCourseAsync(int courseId)
    {
        try
        {
            SetAuthHeader();
            var request = new EnrollmentRequest { CourseId = courseId };
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/enrollments/enroll", request);
            return response.IsSuccessStatusCode;
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
            return response.IsSuccessStatusCode;
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
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
}