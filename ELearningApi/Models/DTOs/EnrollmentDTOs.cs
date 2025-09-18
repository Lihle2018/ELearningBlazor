using System.ComponentModel.DataAnnotations;

namespace ELearningApi.Models.DTOs;

public class EnrollmentRequest
{
    [Required]
    public int CourseId { get; set; }
}

public class EnrollmentDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public double Progress { get; set; }
    public DateTime? LastAccessed { get; set; }
    public bool IsActive { get; set; }
    public List<int> CompletedModules { get; set; } = new();
    public CourseListDto? Course { get; set; }
}

public class ProgressUpdateRequest
{
    [Required]
    public int CourseId { get; set; }

    [Range(0, 100)]
    public double Progress { get; set; }

    public List<int> CompletedModules { get; set; } = new();
}