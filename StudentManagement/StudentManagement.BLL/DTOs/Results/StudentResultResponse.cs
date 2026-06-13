namespace StudentManagement.BLL.DTOs.Results;

public class StudentResultResponse
{
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public decimal AttendancePercentage { get; set; }
    public decimal MidtermScore { get; set; }
    public decimal FinalScore { get; set; }
    public decimal AverageScore { get; set; }
    public string Result { get; set; } = string.Empty;
}
