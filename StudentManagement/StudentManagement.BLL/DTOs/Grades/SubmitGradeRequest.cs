using System.ComponentModel.DataAnnotations;

namespace StudentManagement.BLL.DTOs.Grades;

public class StudentGradeItem
{
    [Required]
    public int StudentId { get; set; }

    [Required]
    [Range(0, 10)]
    public decimal MidTermScore { get; set; }

    [Required]
    [Range(0, 10)]
    public decimal FinalTermScore { get; set; }
}

public class SubmitGradeRequest
{
    [Required]
    public int ClassId { get; set; }

    [Required]
    public List<StudentGradeItem> Records { get; set; } = new();
}

