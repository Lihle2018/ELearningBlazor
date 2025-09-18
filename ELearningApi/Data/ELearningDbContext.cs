using ELearningApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ELearningApi.Data;

public class ELearningDbContext : DbContext
{
    public ELearningDbContext(DbContextOptions<ELearningDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Student entity
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.HasIndex(s => s.Email).IsUnique();
            entity.Property(s => s.Email).IsRequired().HasMaxLength(255);
            entity.Property(s => s.Name).IsRequired().HasMaxLength(255);
            entity.Property(s => s.PasswordHash).IsRequired();
        });

        // Configure Course entity
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Heading).IsRequired().HasMaxLength(255);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(255);
            entity.Property(c => c.Price).HasColumnType("decimal(18,2)");
            entity.Property(c => c.WhatYouLearnJson).HasDefaultValue("[]");
            entity.Property(c => c.RequirementsJson).HasDefaultValue("[]");
            entity.Property(c => c.ModulesJson).HasDefaultValue("[]");
        });

        // Configure Enrollment entity
        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CompletedModulesJson).HasDefaultValue("[]");

            // Configure relationships
            entity.HasOne(e => e.Student)
                  .WithMany(s => s.Enrollments)
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Course)
                  .WithMany(c => c.Enrollments)
                  .HasForeignKey(e => e.CourseId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Ensure unique enrollment per student-course combination
            entity.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}