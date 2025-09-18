using System.ComponentModel.DataAnnotations;

namespace ELearningApi.Models;

public class Student
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime JoinDate { get; set; } = DateTime.UtcNow;

    public string? ProfileImage { get; set; }

    // Navigation properties
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}