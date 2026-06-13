using System.ComponentModel.DataAnnotations;

namespace StudentManagement.BLL.DTOs.Grades;

public class CreateGradeRequest
{
    [Required]
    public int StudentId { get; set; }
    
    [Required]
    public int ClassId { get; set; }
    
    [Required]
    public decimal MidTermScore { get; set; }
    
    [Required]
    public decimal FinalTermScore { get; set; }
}
