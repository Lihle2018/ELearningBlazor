using ELearningBlazor.Models;
using Microsoft.JSInterop;

namespace ELearningBlazor.Services;

public class AuthService : IAuthService
{
    private User? _currentUser;
    private string? _token;
    private readonly IJSRuntime _jsRuntime;

    public event Action? OnAuthStateChanged;

    public User? CurrentUser => _currentUser;
    public bool IsAuthenticated => _currentUser != null;

    public AuthService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        LoadUserFromStorage();
    }

    private async void LoadUserFromStorage()
    {
        try
        {
            _token = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "token");
            var userJson = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "currentUser");

            if (!string.IsNullOrEmpty(userJson))
            {
                _currentUser = System.Text.Json.JsonSerializer.Deserialize<User>(userJson);
                OnAuthStateChanged?.Invoke();
            }
        }
        catch (Exception)
        {
            // Handle localStorage access errors
        }
    }

    public string? GetToken() => _token;

    public async Task<bool> LoginWithApiAsync(ApiService apiService, string email, string password)
    {
        var request = new LoginRequest { Email = email, Password = password };
        var response = await apiService.LoginAsync(request);

        if (response.Success && response.Student != null && !string.IsNullOrEmpty(response.Token))
        {
            _token = response.Token;
            _currentUser = new User
            {
                Id = response.Student.Id,
                Email = response.Student.Email,
                Name = response.Student.Name,
                JoinDate = response.Student.JoinDate,
                ProfileImage = response.Student.ProfileImage ?? "/images/mentor/user1.png"
            };

            await SaveUserToStorage();
            OnAuthStateChanged?.Invoke();
            return true;
        }

        return false;
    }

    public async Task<bool> RegisterWithApiAsync(ApiService apiService, string email, string name, string password)
    {
        var request = new RegisterRequest { Email = email, Name = name, Password = password };
        var response = await apiService.RegisterAsync(request);

        if (response.Success && response.Student != null && !string.IsNullOrEmpty(response.Token))
        {
            _token = response.Token;
            _currentUser = new User
            {
                Id = response.Student.Id,
                Email = response.Student.Email,
                Name = response.Student.Name,
                JoinDate = response.Student.JoinDate,
                ProfileImage = response.Student.ProfileImage ?? "/images/mentor/user3.png"
            };

            await SaveUserToStorage();
            OnAuthStateChanged?.Invoke();
            return true;
        }

        return false;
    }

    private async Task SaveUserToStorage()
    {
        try
        {
            if (_currentUser != null && !string.IsNullOrEmpty(_token))
            {
                var userJson = System.Text.Json.JsonSerializer.Serialize(_currentUser);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "currentUser", userJson);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "token", _token);
            }
        }
        catch (Exception)
        {
            // Handle localStorage access errors
        }
    }

    public async Task LogoutAsync()
    {
        _currentUser = null;
        _token = null;

        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "currentUser");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "token");
        }
        catch (Exception)
        {
            // Handle localStorage access errors
        }

        OnAuthStateChanged?.Invoke();
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        // Create temporary ApiService for login
        var httpClient = new HttpClient();
        var apiService = new ApiService(httpClient, this);
        return await LoginWithApiAsync(apiService, email, password);
    }

    public async Task<bool> RegisterAsync(string email, string name, string password)
    {
        // Create temporary ApiService for registration
        var httpClient = new HttpClient();
        var apiService = new ApiService(httpClient, this);
        return await RegisterWithApiAsync(apiService, email, name, password);
    }

}