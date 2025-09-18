using ELearningBlazor.Models;

namespace ELearningBlazor.Services;

public class DataService
{
    private readonly ApiService _apiService;

    public DataService(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<List<Course>> GetCoursesAsync()
    {
        var courseDtos = await _apiService.GetCoursesAsync();
        return courseDtos.Select(dto => new Course
        {
            Id = dto.Id,
            Heading = dto.Heading,
            Name = dto.Name,
            ImgSrc = dto.ImgSrc,
            Students = dto.Students,
            Classes = dto.Classes,
            Price = dto.Price,
            Rating = dto.Rating,
            Description = dto.Description,
            Duration = dto.Duration,
            Level = dto.Level
        }).ToList();
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        var courseDto = await _apiService.GetCourseAsync(id);
        if (courseDto == null) return null;

        return new Course
        {
            Id = courseDto.Id,
            Heading = courseDto.Heading,
            Name = courseDto.Name,
            ImgSrc = courseDto.ImgSrc,
            Students = courseDto.Students,
            Classes = courseDto.Classes,
            Price = courseDto.Price,
            Rating = courseDto.Rating,
            Description = courseDto.Description,
            LongDescription = courseDto.LongDescription,
            Duration = courseDto.Duration,
            Level = courseDto.Level,
            Language = courseDto.Language,
            HasCertificate = courseDto.HasCertificate,
            InstructorBio = courseDto.InstructorBio,
            InstructorImage = courseDto.InstructorImage,
            WhatYouLearn = courseDto.WhatYouLearn,
            Requirements = courseDto.Requirements,
            Modules = courseDto.Modules
        };
    }

    // Legacy static methods for backward compatibility
    public static List<Course> GetCourses()
    {
        // Return hardcoded data for components that haven't been updated yet
        return GetHardcodedCourses();
    }

    public static Course? GetCourseById(int id)
    {
        return GetHardcodedCourses().FirstOrDefault(c => c.Id == id);
    }

    private static List<Course> GetHardcodedCourses()
    {
        return new List<Course>
        {
            new()
            {
                Id = 1,
                Heading = "Full stack modern javascript",
                Name = "Colt Stelle",
                ImgSrc = "/images/courses/courseone.png",
                Students = 150,
                Classes = 12,
                Price = 0,
                Rating = 4.4,
                Description = "Master modern JavaScript development with React, Node.js, and MongoDB",
                LongDescription = "This comprehensive course covers everything you need to become a full-stack JavaScript developer.",
                Duration = "8 weeks",
                Level = "Intermediate",
                Language = "English",
                InstructorBio = "Colt Stelle is a senior software engineer with 8+ years of experience in JavaScript development.",
                InstructorImage = "/images/mentor/user1.png",
                WhatYouLearn = new List<string>
                {
                    "Modern JavaScript (ES6+) fundamentals",
                    "React.js for building user interfaces",
                    "Node.js for backend development"
                },
                Requirements = new List<string>
                {
                    "Basic understanding of HTML and CSS",
                    "Computer with internet connection"
                },
                Modules = new List<CourseModule>
                {
                    new() { Title = "JavaScript Fundamentals", Duration = "1 week", Lessons = new() { "Variables and Data Types", "Functions and Scope" } },
                    new() { Title = "React Development", Duration = "2 weeks", Lessons = new() { "Components and Props", "State Management" } }
                }
            },
            new()
            {
                Id = 2,
                Heading = "Design system with React programme",
                Name = "Colt Stelle",
                ImgSrc = "/images/courses/coursetwo.png",
                Students = 130,
                Classes = 12,
                Price = 0,
                Rating = 4.5,
                Description = "Learn to build scalable and maintainable design systems using React components",
                Duration = "6 weeks",
                Level = "Advanced"
            },
            new()
            {
                Id = 3,
                Heading = "Design banner with Figma",
                Name = "Colt Stelle",
                ImgSrc = "/images/courses/coursethree.png",
                Students = 120,
                Classes = 12,
                Price = 0,
                Rating = 5,
                Description = "Master Figma to create stunning banners and marketing materials",
                Duration = "4 weeks",
                Level = "Beginner"
            }
        };
    }

    public static List<Company> GetTrustedCompanies()
    {
        return new List<Company>
        {
            new() { ImgSrc = "/images/companies/airbnb.svg" },
            new() { ImgSrc = "/images/companies/fedex.svg" },
            new() { ImgSrc = "/images/companies/google.svg" },
            new() { ImgSrc = "/images/companies/hubspot.svg" },
            new() { ImgSrc = "/images/companies/microsoft.svg" },
            new() { ImgSrc = "/images/companies/walmart.svg" },
            new() { ImgSrc = "/images/companies/airbnb.svg" },
            new() { ImgSrc = "/images/companies/fedex.svg" }
        };
    }


    public static List<Mentor> GetMentors()
    {
        return new List<Mentor>
        {
            new()
            {
                Profession = "Senior UX Designer",
                Name = "Shoo Thar Mien",
                ImgSrc = "/images/mentor/user3.png"
            },
            new()
            {
                Profession = "Senior UX Designer",
                Name = "Shoo Thar Mien",
                ImgSrc = "/images/mentor/user2.png"
            },
            new()
            {
                Profession = "Senior UX Designer",
                Name = "Shoo Thar Mien",
                ImgSrc = "/images/mentor/user1.png"
            },
            new()
            {
                Profession = "Senior UX Designer",
                Name = "Shoo Thar Mien",
                ImgSrc = "/images/mentor/user3.png"
            },
            new()
            {
                Profession = "Senior UX Designer",
                Name = "Shoo Thar Mien",
                ImgSrc = "/images/mentor/user2.png"
            },
            new()
            {
                Profession = "Senior UX Designer",
                Name = "Shoo Thar Mien",
                ImgSrc = "/images/mentor/user1.png"
            }
        };
    }

    public static List<Testimonial> GetTestimonials()
    {
        return new List<Testimonial>
        {
            new()
            {
                Name = "Robert Fox",
                Profession = "CEO, Parkview Int.Ltd",
                Comment = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour",
                ImgSrc = "/images/testimonial/user.svg",
                Rating = 5
            },
            new()
            {
                Name = "Leslie Alexander",
                Profession = "CEO, Parkview Int.Ltd",
                Comment = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour",
                ImgSrc = "/images/mentor/user2.png",
                Rating = 5
            },
            new()
            {
                Name = "Cody Fisher",
                Profession = "CEO, Parkview Int.Ltd",
                Comment = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour",
                ImgSrc = "/images/mentor/user3.png",
                Rating = 5
            },
            new()
            {
                Name = "Robert Fox",
                Profession = "CEO, Parkview Int.Ltd",
                Comment = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour",
                ImgSrc = "/images/mentor/user1.png",
                Rating = 5
            },
            new()
            {
                Name = "Leslie Alexander",
                Profession = "CEO, Parkview Int.Ltd",
                Comment = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour",
                ImgSrc = "/images/mentor/user2.png",
                Rating = 5
            },
            new()
            {
                Name = "Cody Fisher",
                Profession = "CEO, Parkview Int.Ltd",
                Comment = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour",
                ImgSrc = "/images/mentor/user3.png",
                Rating = 5
            }
        };
    }
}