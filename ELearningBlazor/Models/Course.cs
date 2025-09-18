namespace ELearningBlazor.Models;

public class Course
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
    public List<string> WhatYouLearn { get; set; } = new();
    public List<string> Requirements { get; set; } = new();
    public string Duration { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public bool HasCertificate { get; set; } = true;
    public List<CourseModule> Modules { get; set; } = new();
    public string InstructorBio { get; set; } = string.Empty;
    public string InstructorImage { get; set; } = string.Empty;
}

public class CourseModule
{
    public string Title { get; set; } = string.Empty;
    public List<string> Lessons { get; set; } = new();
    public string Duration { get; set; } = string.Empty;
}

public class Company
{
    public string ImgSrc { get; set; } = string.Empty;
}

public class Mentor
{
    public string Profession { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ImgSrc { get; set; } = string.Empty;
}

public class Testimonial
{
    public string Profession { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public string ImgSrc { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Rating { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime JoinDate { get; set; } = DateTime.Now;
    public string ProfileImage { get; set; } = "/images/mentor/user1.png";
    public List<EnrolledCourse> EnrolledCourses { get; set; } = new();
}

public class EnrolledCourse
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    public double Progress { get; set; } = 0.0;
    public List<int> CompletedModules { get; set; } = new();
    public DateTime? LastAccessed { get; set; }
    public bool IsActive { get; set; } = true;
    public Course? Course { get; set; }
}