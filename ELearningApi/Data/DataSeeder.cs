using ELearningApi.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace ELearningApi.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(ELearningDbContext context)
    {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Seed Courses
        if (!await context.Courses.AnyAsync())
        {
            var courses = new List<Course>
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
                    LongDescription = "This comprehensive course covers everything you need to become a full-stack JavaScript developer. From frontend development with React to backend APIs with Node.js and database management with MongoDB, you'll build real-world applications and gain the skills needed to excel in today's tech industry.",
                    Duration = "8 weeks",
                    Level = "Intermediate",
                    Language = "English",
                    InstructorBio = "Colt Stelle is a senior software engineer with 8+ years of experience in JavaScript development.",
                    InstructorImage = "/images/mentor/user1.png",
                    WhatYouLearn = new List<string>
                    {
                        "Modern JavaScript (ES6+) fundamentals",
                        "React.js for building user interfaces",
                        "Node.js for backend development",
                        "MongoDB database integration",
                        "RESTful API development",
                        "Authentication and authorization",
                        "Deployment strategies"
                    },
                    Requirements = new List<string>
                    {
                        "Basic understanding of HTML and CSS",
                        "Computer with internet connection",
                        "Willingness to learn and practice"
                    },
                    Modules = new List<CourseModule>
                    {
                        new() { Title = "JavaScript Fundamentals", Duration = "1 week", Lessons = new() { "Variables and Data Types", "Functions and Scope", "Objects and Arrays", "Async Programming" } },
                        new() { Title = "React Development", Duration = "2 weeks", Lessons = new() { "Components and Props", "State Management", "Event Handling", "Routing" } },
                        new() { Title = "Backend with Node.js", Duration = "2 weeks", Lessons = new() { "Express Framework", "Middleware", "Database Integration", "Authentication" } },
                        new() { Title = "Full Stack Project", Duration = "3 weeks", Lessons = new() { "Project Planning", "API Development", "Frontend Integration", "Deployment" } }
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
                    LongDescription = "Create professional design systems from scratch using React. This course teaches you how to build reusable components, establish design tokens, and create comprehensive style guides that scale across large applications.",
                    Duration = "6 weeks",
                    Level = "Advanced",
                    Language = "English",
                    InstructorBio = "Colt Stelle specializes in React development and design system architecture.",
                    InstructorImage = "/images/mentor/user1.png",
                    WhatYouLearn = new List<string>
                    {
                        "Design system principles and architecture",
                        "Building reusable React components",
                        "Design tokens and theming",
                        "Storybook for component documentation",
                        "Testing design systems",
                        "Design system governance"
                    },
                    Requirements = new List<string>
                    {
                        "Solid understanding of React",
                        "Experience with CSS and styling",
                        "Familiarity with JavaScript ES6+"
                    },
                    Modules = new List<CourseModule>
                    {
                        new() { Title = "Design System Foundations", Duration = "1 week", Lessons = new() { "What is a Design System", "Component Architecture", "Design Tokens" } },
                        new() { Title = "Building Components", Duration = "2 weeks", Lessons = new() { "Button Components", "Form Elements", "Layout Components" } },
                        new() { Title = "Documentation & Testing", Duration = "2 weeks", Lessons = new() { "Storybook Setup", "Component Testing", "Visual Regression Testing" } },
                        new() { Title = "Implementation", Duration = "1 week", Lessons = new() { "Publishing to NPM", "Integration Strategies", "Maintenance" } }
                    }
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
                    LongDescription = "Learn professional banner design techniques using Figma. From basic layouts to advanced animations, you'll master the tools and techniques used by professional designers to create eye-catching marketing materials.",
                    Duration = "4 weeks",
                    Level = "Beginner",
                    Language = "English",
                    InstructorBio = "Colt Stelle is a UX/UI designer with expertise in Figma and visual design.",
                    InstructorImage = "/images/mentor/user1.png",
                    WhatYouLearn = new List<string>
                    {
                        "Figma interface and tools",
                        "Typography and color theory",
                        "Layout and composition principles",
                        "Creating professional banners",
                        "Export settings and formats",
                        "Collaboration in Figma"
                    },
                    Requirements = new List<string>
                    {
                        "No prior design experience needed",
                        "Computer with internet access",
                        "Free Figma account"
                    },
                    Modules = new List<CourseModule>
                    {
                        new() { Title = "Figma Basics", Duration = "1 week", Lessons = new() { "Interface Overview", "Basic Tools", "Frames and Layers" } },
                        new() { Title = "Design Principles", Duration = "1 week", Lessons = new() { "Color Theory", "Typography", "Layout Principles" } },
                        new() { Title = "Banner Creation", Duration = "1 week", Lessons = new() { "Social Media Banners", "Web Banners", "Print Materials" } },
                        new() { Title = "Advanced Techniques", Duration = "1 week", Lessons = new() { "Prototyping", "Auto-layout", "Components and Variants" } }
                    }
                }
            };

            context.Courses.AddRange(courses);
            await context.SaveChangesAsync();
        }

        // Seed Students
        if (!await context.Students.AnyAsync())
        {
            var students = new List<Student>
            {
                new()
                {
                    Email = "john.doe@example.com",
                    Name = "John Doe",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    ProfileImage = "/images/mentor/user1.png",
                    JoinDate = DateTime.UtcNow.AddMonths(-3)
                },
                new()
                {
                    Email = "jane.smith@example.com",
                    Name = "Jane Smith",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password456"),
                    ProfileImage = "/images/mentor/user2.png",
                    JoinDate = DateTime.UtcNow.AddMonths(-6)
                }
            };

            context.Students.AddRange(students);
            await context.SaveChangesAsync();
        }

        // Seed Enrollments
        if (!await context.Enrollments.AnyAsync())
        {
            var enrollments = new List<Enrollment>
            {
                new()
                {
                    StudentId = 1,
                    CourseId = 1,
                    Progress = 0,
                    CompletedModules = new List<int>(),
                    LastAccessed = DateTime.UtcNow.AddDays(-2),
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-2)
                },
                new()
                {
                    StudentId = 1,
                    CourseId = 2,
                    Progress = 0,
                    CompletedModules = new List<int>(),
                    LastAccessed = DateTime.UtcNow.AddDays(-5),
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-1)
                }
            };

            context.Enrollments.AddRange(enrollments);
            await context.SaveChangesAsync();
        }
    }
}