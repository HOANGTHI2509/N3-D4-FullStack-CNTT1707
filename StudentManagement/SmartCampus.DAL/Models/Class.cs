using System;
using System.Collections.Generic;

namespace SmartCampus.DAL.Models;

public partial class Class
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string ClassName { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? MaxStudents { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;
}
