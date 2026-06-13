using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Application.DTOs.Students;

public class UpdateStudentRequest
{
    [MaxLength(20)]
    public string? IdentityCardNumber { get; set; }

    [MaxLength(100)]
    public string? FullName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    [MaxLength(10)]
    public string? Gender { get; set; }

    [EmailAddress]
    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [MaxLength(255)]
    public string? Address { get; set; }

    [MaxLength(500)]
    public string? AvatarUrl { get; set; }

    [MaxLength(100)]
    public string? Major { get; set; }

    [MaxLength(50)]
    public string? Status { get; set; }
}

