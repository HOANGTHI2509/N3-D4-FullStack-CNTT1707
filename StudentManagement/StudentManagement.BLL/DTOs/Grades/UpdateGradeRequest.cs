using System.ComponentModel.DataAnnotations;

namespace StudentManagement.BLL.DTOs.Grades;

public class UpdateGradeRequest
{
    [Required]
    public decimal MidTermScore { get; set; }
    
    [Required]
    public decimal FinalTermScore { get; set; }
}
