using System.ComponentModel.DataAnnotations;

namespace StudentManagement.BLL.DTOs.Students;

public class CreateStudentRequest
{
    [Required]
    [MaxLength(20)]
    public string StudentCode { get; set; } = null!;

    [MaxLength(20)]
    public string? IdentityCardNumber { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = null!;

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
}

