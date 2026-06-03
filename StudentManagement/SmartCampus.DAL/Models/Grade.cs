using System;
using System.Collections.Generic;

namespace SmartCampus.DAL.Models;

public partial class Grade
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int ClassId { get; set; }

    public string GradeType { get; set; } = null!;

    public decimal? Score { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Student Student { get; set; } = null!;
}
