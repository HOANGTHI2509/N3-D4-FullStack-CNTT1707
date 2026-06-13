using System;
using System.Collections.Generic;

namespace StudentManagement.DAL.Models;

public partial class Enrollment
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int ClassId { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public string? Status { get; set; }

    public decimal? AttendancePercentage { get; set; }

    public string? FinalResult { get; set; }

    public virtual Student Student { get; set; } = null!;
}

