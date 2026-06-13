using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.DTOs;
namespace StudentManagement.Application.DTOs.Enrollments;

public class ClassStudentResponse
{
    public int EnrollmentId { get; set; }

    public int StudentId { get; set; }

    public string StudentCode { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string Status { get; set; } = string.Empty;
}

