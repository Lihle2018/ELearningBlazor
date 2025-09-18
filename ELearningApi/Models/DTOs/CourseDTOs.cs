namespace ELearningApi.Models.DTOs;

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