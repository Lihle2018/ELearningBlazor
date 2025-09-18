using ELearningApi.Data;
using ELearningApi.Models;
using ELearningApi.Models.DTOs;
using ELearningApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace ELearningApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ELearningDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthController(ELearningDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthResponse
            {
                Success = false,
                Message = "Invalid data provided"
            });
        }

        // Check if email already exists
        if (await _context.Students.AnyAsync(s => s.Email == request.Email))
        {
            return BadRequest(new AuthResponse
            {
                Success = false,
                Message = "Email already registered"
            });
        }

        // Create new student
        var student = new Student
        {
            Email = request.Email,
            Name = request.Name,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            ProfileImage = "/images/mentor/user3.png",
            JoinDate = DateTime.UtcNow
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        // Generate JWT token
        var token = _jwtService.GenerateToken(student);

        return Ok(new AuthResponse
        {
            Success = true,
            Message = "Registration successful",
            Token = token,
            Student = new StudentDto
            {
                Id = student.Id,
                Email = student.Email,
                Name = student.Name,
                JoinDate = student.JoinDate,
                ProfileImage = student.ProfileImage
            }
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new AuthResponse
            {
                Success = false,
                Message = "Invalid data provided"
            });
        }

        // Find student by email
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == request.Email);
        if (student == null || !BCrypt.Net.BCrypt.Verify(request.Password, student.PasswordHash))
        {
            return BadRequest(new AuthResponse
            {
                Success = false,
                Message = "Invalid email or password"
            });
        }

        // Generate JWT token
        var token = _jwtService.GenerateToken(student);

        return Ok(new AuthResponse
        {
            Success = true,
            Message = "Login successful",
            Token = token,
            Student = new StudentDto
            {
                Id = student.Id,
                Email = student.Email,
                Name = student.Name,
                JoinDate = student.JoinDate,
                ProfileImage = student.ProfileImage
            }
        });
    }
}