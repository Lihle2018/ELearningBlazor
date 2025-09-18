using ELearningBlazor.Models;

namespace ELearningBlazor.Services;

public interface IAuthService
{
    User? CurrentUser { get; }
    bool IsAuthenticated { get; }

    event Action? OnAuthStateChanged;

    string? GetToken();
    Task<bool> LoginAsync(string email, string password);
    Task<bool> RegisterAsync(string email, string name, string password);
    Task LogoutAsync();
}