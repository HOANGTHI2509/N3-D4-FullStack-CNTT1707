namespace StudentManagement.BLL.DTOs.Grades;

public class StudentGradeResponse
{
    public int StudentId { get; set; }
    public int ClassId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public GradeScores Scores { get; set; } = new();
}

public class GradeScores
{
    public decimal Attendance { get; set; }
    public decimal Midterm { get; set; }
    public decimal Final { get; set; }
    public decimal Average { get; set; }
}
