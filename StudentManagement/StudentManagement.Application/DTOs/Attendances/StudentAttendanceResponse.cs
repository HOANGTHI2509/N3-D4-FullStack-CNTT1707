using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.DTOs;
namespace StudentManagement.Application.DTOs.Attendances;

public class StudentAttendanceResponse
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public DateOnly AttendanceDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Note { get; set; }
}

public class AttendanceSummaryResponse
{
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public int TotalSessions { get; set; }
    public int PresentSessions { get; set; }
    public decimal AttendancePercentage { get; set; }
}
