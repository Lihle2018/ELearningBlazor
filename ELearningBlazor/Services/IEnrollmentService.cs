using ELearningBlazor.Models;

namespace ELearningBlazor.Services;

public interface IEnrollmentService
{
    Task<List<EnrolledCourse>> GetMyEnrollmentsAsync();
    Task<bool> EnrollInCourseAsync(int courseId);
    Task<bool> UnenrollFromCourseAsync(int courseId);
    Task<bool> UpdateProgressAsync(int courseId, double progress, List<int> completedModules);
    EnrolledCourse? GetEnrollmentInfo(int courseId);
    List<Course> GetEnrolledCourses();
}