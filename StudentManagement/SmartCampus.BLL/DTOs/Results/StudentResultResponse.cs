namespace SmartCampus.BLL.DTOs.Results;

public class StudentResultResponse
{
    public int StudentId { get; set; }

    public int ClassId { get; set; }

    public int TotalAttendanceSessions { get; set; }

    public int AttendedSessions { get; set; }

    public decimal AttendancePercentage { get; set; }

    public decimal? AverageScore { get; set; }

    public string Classification { get; set; } = string.Empty;

    public bool IsPassed { get; set; }
}
