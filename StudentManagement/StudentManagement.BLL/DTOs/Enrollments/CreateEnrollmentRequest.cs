using System.ComponentModel.DataAnnotations;

namespace StudentManagement.BLL.DTOs.Enrollments;

public class CreateEnrollmentRequest
{
    [Range(1, int.MaxValue)]
    public int StudentId { get; set; }

    [Range(1, int.MaxValue)]
    public int ClassId { get; set; }
}

