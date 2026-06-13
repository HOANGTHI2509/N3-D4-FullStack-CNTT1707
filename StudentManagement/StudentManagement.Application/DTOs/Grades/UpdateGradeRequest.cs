using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Application.DTOs.Grades;

public class UpdateGradeRequest
{
    [Required]
    public decimal MidTermScore { get; set; }
    
    [Required]
    public decimal FinalTermScore { get; set; }
}
