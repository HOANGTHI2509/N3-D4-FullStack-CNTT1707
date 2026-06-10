using System.ComponentModel.DataAnnotations;

namespace SmartCampus.BLL.DTOs.Attendances;

public class CreateAttendanceRequest
{
    [Range(1, int.MaxValue)]
    public int StudentId { get; set; }

    [Range(1, int.MaxValue)]
    public int ClassId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Note { get; set; }
}
