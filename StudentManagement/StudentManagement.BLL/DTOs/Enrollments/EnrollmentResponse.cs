namespace StudentManagement.BLL.DTOs.Enrollments;

public class EnrollmentResponse
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int ClassId { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public string Status { get; set; } = string.Empty;
}

