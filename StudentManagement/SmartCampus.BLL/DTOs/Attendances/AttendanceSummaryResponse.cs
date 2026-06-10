namespace SmartCampus.BLL.DTOs.Attendances;

public class AttendanceSummaryResponse
{
    public int StudentId { get; set; }

    public int ClassId { get; set; }

    public int TotalSessions { get; set; }

    public int AttendedSessions { get; set; }

    public decimal AttendancePercentage { get; set; }
}
