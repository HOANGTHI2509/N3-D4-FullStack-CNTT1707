namespace SmartCampus.BLL.DTOs.Grades;

public class GradeResponse
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int ClassId { get; set; }

    public string GradeType { get; set; } = string.Empty;

    public decimal? Score { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }
}
