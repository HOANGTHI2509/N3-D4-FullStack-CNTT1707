using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.DTOs;
namespace StudentManagement.Application.DTOs.Enrollments;

public class EnrollmentResponse
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int ClassId { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public string Status { get; set; } = string.Empty;
}

