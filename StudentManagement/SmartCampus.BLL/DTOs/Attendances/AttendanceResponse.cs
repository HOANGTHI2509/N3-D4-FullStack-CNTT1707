namespace SmartCampus.BLL.DTOs.Attendances;

public class AttendanceResponse
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int ClassId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string? Note { get; set; }
}
