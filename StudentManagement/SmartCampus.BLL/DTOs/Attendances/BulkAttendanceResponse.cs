namespace SmartCampus.BLL.DTOs.Attendances;

public class BulkAttendanceResponse
{
    public int ClassId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public int CreatedCount { get; set; }

    public IReadOnlyList<AttendanceResponse> CreatedAttendances { get; set; } = [];
}
