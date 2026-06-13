using StudentManagement.Domain.Entities;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Application.DTOs.Enrollments;

public class CreateEnrollmentRequest
{
    [Range(1, int.MaxValue)]
    public int StudentId { get; set; }

    [Range(1, int.MaxValue)]
    public int ClassId { get; set; }
}

