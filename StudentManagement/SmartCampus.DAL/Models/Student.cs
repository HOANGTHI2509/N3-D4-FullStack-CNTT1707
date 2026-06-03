using System;
using System.Collections.Generic;

namespace SmartCampus.DAL.Models;

public partial class Student
{
    public int Id { get; set; }

    public string StudentCode { get; set; } = null!;

    public string? IdentityCardNumber { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? AvatarUrl { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    public string? Major { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
