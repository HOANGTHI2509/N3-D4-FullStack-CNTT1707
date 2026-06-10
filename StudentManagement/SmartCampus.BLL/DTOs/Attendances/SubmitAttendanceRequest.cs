using System.ComponentModel.DataAnnotations;

namespace SmartCampus.BLL.DTOs.Attendances;

public class StudentAttendanceItem
{
    [Required]
    public int StudentId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = null!; // "Present" or "Absent"

    [MaxLength(255)]
    public string? Note { get; set; }
}

public class SubmitAttendanceRequest
{
    [Required]
    public int ClassId { get; set; }

    [Required]
    public DateOnly AttendanceDate { get; set; }

    [Required]
    public List<StudentAttendanceItem> Records { get; set; } = new();
}
