using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Application.DTOs.Attendances;

public class CreateAttendanceRequest
{
    [Required]
    public int StudentId { get; set; }
    
    [Required]
    public int ClassId { get; set; }
    
    [Required]
    public DateOnly AttendanceDate { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = null!;
    
    [MaxLength(255)]
    public string? Note { get; set; }
}
