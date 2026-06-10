using SmartCampus.BLL.Services;
using SmartCampus.DAL.Models;
using Xunit;

namespace SmartCampus.BLL.Tests;

public class AcademicResultCalculatorTests
{
    [Fact]
    public void Calculate_ReturnsPassedExcellent_WhenAttendanceAndAverageScoreAreHigh()
    {
        var attendances = new List<Attendance>
        {
            CreateAttendance("Present"),
            CreateAttendance("Present"),
            CreateAttendance("Late"),
            CreateAttendance("Absent"),
            CreateAttendance("Excused")
        };
        var grades = new List<Grade>
        {
            CreateGrade(8),
            CreateGrade(9)
        };

        var result = AcademicResultCalculator.Calculate(1, 10, attendances, grades);

        Assert.Equal(80, result.AttendancePercentage);
        Assert.Equal(8.5m, result.AverageScore);
        Assert.True(result.IsPassed);
        Assert.Equal("Gioi", result.Classification);
    }

    [Fact]
    public void Calculate_ReturnsFailed_WhenAttendanceIsTooLow()
    {
        var attendances = new List<Attendance>
        {
            CreateAttendance("Present"),
            CreateAttendance("Absent"),
            CreateAttendance("Absent")
        };
        var grades = new List<Grade>
        {
            CreateGrade(9)
        };

        var result = AcademicResultCalculator.Calculate(1, 10, attendances, grades);

        Assert.Equal(33.33m, result.AttendancePercentage);
        Assert.False(result.IsPassed);
        Assert.Equal("Khong dat", result.Classification);
    }

    [Fact]
    public void Calculate_ReturnsFailed_WhenAverageScoreIsMissing()
    {
        var attendances = new List<Attendance>
        {
            CreateAttendance("Present"),
            CreateAttendance("Present")
        };
        var grades = new List<Grade>();

        var result = AcademicResultCalculator.Calculate(1, 10, attendances, grades);

        Assert.Null(result.AverageScore);
        Assert.False(result.IsPassed);
        Assert.Equal("Khong dat", result.Classification);
    }

    private static Attendance CreateAttendance(string status)
    {
        return new Attendance
        {
            StudentId = 1,
            ClassId = 10,
            AttendanceDate = DateOnly.FromDateTime(DateTime.UtcNow),
            Status = status
        };
    }

    private static Grade CreateGrade(decimal score)
    {
        return new Grade
        {
            StudentId = 1,
            ClassId = 10,
            GradeType = "Quiz",
            Score = score
        };
    }
}
