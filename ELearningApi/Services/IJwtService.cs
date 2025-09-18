using ELearningApi.Models;

namespace ELearningApi.Services;

public interface IJwtService
{
    string GenerateToken(Student student);
    bool ValidateToken(string token);
    int? GetStudentIdFromToken(string token);
}