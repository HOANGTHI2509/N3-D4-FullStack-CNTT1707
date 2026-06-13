using System;
using System.Collections.Generic;

namespace StudentManagement.DAL.Models;

public partial class Attendance
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int ClassId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Note { get; set; }

    public virtual Student Student { get; set; } = null!;
}

