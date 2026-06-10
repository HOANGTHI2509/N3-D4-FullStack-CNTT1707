using System.ComponentModel.DataAnnotations;

namespace SmartCampus.BLL.DTOs.Students;

public class UpdateStudentRequest
{
    [MaxLength(15)]
    public string? PhoneNumber { get; set; }

    [MaxLength(255)]
    public string? Address { get; set; }

    [EmailAddress]
    [MaxLength(100)]
    public string? Email { get; set; }
}
