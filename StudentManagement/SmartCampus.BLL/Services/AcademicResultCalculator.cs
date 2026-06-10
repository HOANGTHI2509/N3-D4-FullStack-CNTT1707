using SmartCampus.BLL.DTOs.Results;
using SmartCampus.DAL.Models;

namespace SmartCampus.BLL.Services;

public static class AcademicResultCalculator
{
    public static StudentResultResponse Calculate(
        int studentId,
        int classId,
        IReadOnlyCollection<Attendance> attendances,
        IReadOnlyCollection<Grade> grades)
    {
        var attendanceSummary = AttendanceService.CalculateSummary(studentId, classId, attendances);

        var validScores = grades
            .Where(grade => grade.Score.HasValue)
            .Select(grade => grade.Score!.Value)
            .ToList();

        decimal? averageScore = validScores.Count == 0
            ? null
            : Math.Round(validScores.Average(), 2);

        var isPassed = attendanceSummary.AttendancePercentage >= 80 && averageScore >= 5;

        return new StudentResultResponse
        {
            StudentId = studentId,
            ClassId = classId,
            TotalAttendanceSessions = attendanceSummary.TotalSessions,
            AttendedSessions = attendanceSummary.AttendedSessions,
            AttendancePercentage = attendanceSummary.AttendancePercentage,
            AverageScore = averageScore,
            Classification = Classify(averageScore, isPassed),
            IsPassed = isPassed
        };
    }

    private static string Classify(decimal? averageScore, bool isPassed)
    {
        if (!isPassed || averageScore is null)
        {
            return "Khong dat";
        }

        return averageScore switch
        {
            >= 8 => "Gioi",
            >= 6.5m => "Kha",
            >= 5 => "Trung binh",
            _ => "Khong dat"
        };
    }
}
