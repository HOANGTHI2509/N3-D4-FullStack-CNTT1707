using System;
using System.Collections.Generic;

namespace SmartCampus.DAL.Models;

public partial class Course
{
    public int Id { get; set; }

    public string CourseCode { get; set; } = null!;

    public string CourseName { get; set; } = null!;

    public string? Description { get; set; }

    public int? Credits { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
