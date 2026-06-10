using System.ComponentModel.DataAnnotations;

namespace SmartCampus.BLL.DTOs.Attendances;

public class UpdateAttendanceRequest
{
    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Note { get; set; }
}
