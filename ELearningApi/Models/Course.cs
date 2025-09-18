using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApi.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    public string Heading { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    public string ImgSrc { get; set; } = string.Empty;

    public int Students { get; set; }

    public int Classes { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public double Rating { get; set; }

    public string Description { get; set; } = string.Empty;

    public string LongDescription { get; set; } = string.Empty;

    public string Duration { get; set; } = string.Empty;

    public string Level { get; set; } = string.Empty;

    public string Language { get; set; } = string.Empty;

    public bool HasCertificate { get; set; } = true;

    public string InstructorBio { get; set; } = string.Empty;

    public string InstructorImage { get; set; } = string.Empty;

    // JSON stored fields for complex data
    public string WhatYouLearnJson { get; set; } = "[]";
    public string RequirementsJson { get; set; } = "[]";
    public string ModulesJson { get; set; } = "[]";

    // Navigation properties
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    // Helper properties (not mapped to database)
    [NotMapped]
    public List<string> WhatYouLearn
    {
        get => System.Text.Json.JsonSerializer.Deserialize<List<string>>(WhatYouLearnJson) ?? new List<string>();
        set => WhatYouLearnJson = System.Text.Json.JsonSerializer.Serialize(value);
    }

    [NotMapped]
    public List<string> Requirements
    {
        get => System.Text.Json.JsonSerializer.Deserialize<List<string>>(RequirementsJson) ?? new List<string>();
        set => RequirementsJson = System.Text.Json.JsonSerializer.Serialize(value);
    }

    [NotMapped]
    public List<CourseModule> Modules
    {
        get => System.Text.Json.JsonSerializer.Deserialize<List<CourseModule>>(ModulesJson) ?? new List<CourseModule>();
        set => ModulesJson = System.Text.Json.JsonSerializer.Serialize(value);
    }
}

public class CourseModule
{
    public string Title { get; set; } = string.Empty;
    public List<string> Lessons { get; set; } = new();
    public string Duration { get; set; } = string.Empty;
}