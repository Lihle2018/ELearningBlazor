using System.ComponentModel.DataAnnotations;

namespace ELearningBlazor.Models;

// API Request/Response Models
public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

public class RegisterRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}

public class AuthResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Token { get; set; }
    public StudentDto? Student { get; set; }
}

public class StudentDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime JoinDate { get; set; }
    public string? ProfileImage { get; set; }
}

public class CourseDto
{
    public int Id { get; set; }
    public string Heading { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ImgSrc { get; set; } = string.Empty;
    public int Students { get; set; }
    public int Classes { get; set; }
    public decimal Price { get; set; }
    public double Rating { get; set; }
    public string Description { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public bool HasCertificate { get; set; }
    public string InstructorBio { get; set; } = string.Empty;
    public string InstructorImage { get; set; } = string.Empty;
    public List<string> WhatYouLearn { get; set; } = new();
    public List<string> Requirements { get; set; } = new();
    public List<CourseModule> Modules { get; set; } = new();
    public bool IsEnrolled { get; set; }
    public double? Progress { get; set; }
}

public class CourseListDto
{
    public int Id { get; set; }
    public string Heading { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ImgSrc { get; set; } = string.Empty;
    public int Students { get; set; }
    public int Classes { get; set; }
    public decimal Price { get; set; }
    public double Rating { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public bool IsEnrolled { get; set; }
    public double? Progress { get; set; }
}

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