using System.ComponentModel.DataAnnotations;

namespace SmartCampus.BLL.DTOs.Grades;

public class CreateGradeRequest
{
    [Range(1, int.MaxValue)]
    public int StudentId { get; set; }

    [Range(1, int.MaxValue)]
    public int ClassId { get; set; }

    [Required]
    [MaxLength(50)]
    public string GradeType { get; set; } = string.Empty;

    [Range(0, 10)]
    public decimal Score { get; set; }

    [MaxLength(255)]
    public string? Note { get; set; }
}
