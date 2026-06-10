using System.ComponentModel.DataAnnotations;

namespace SmartCampus.BLL.DTOs.Attendances;

public class BulkAttendanceItemRequest
{
    [Range(1, int.MaxValue)]
    public int StudentId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Note { get; set; }
}
