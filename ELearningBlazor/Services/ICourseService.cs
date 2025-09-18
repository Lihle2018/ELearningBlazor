using ELearningBlazor.Models;

namespace ELearningBlazor.Services;

public interface ICourseService
{
    Task<List<Course>> GetCoursesAsync();
    Task<Course?> GetCourseByIdAsync(int id);
}