using System.ComponentModel.DataAnnotations;

namespace ELearningApi.Models;

public class Enrollment
{
    public int Id { get; set; }

    [Required]
    public int StudentId { get; set; }

    [Required]
    public int CourseId { get; set; }

    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

    public double Progress { get; set; } = 0.0;

    public DateTime? LastAccessed { get; set; }

    public bool IsActive { get; set; } = true;

    // JSON stored field for completed modules
    public string CompletedModulesJson { get; set; } = "[]";

    // Navigation properties
    public Student Student { get; set; } = null!;
    public Course Course { get; set; } = null!;

    // Helper property (not mapped to database)
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public List<int> CompletedModules
    {
        get => System.Text.Json.JsonSerializer.Deserialize<List<int>>(CompletedModulesJson) ?? new List<int>();
        set => CompletedModulesJson = System.Text.Json.JsonSerializer.Serialize(value);
    }
}