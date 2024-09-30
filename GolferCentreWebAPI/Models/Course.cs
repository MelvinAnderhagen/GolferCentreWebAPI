using System;
using System.Collections.Generic;

namespace GolferCentreWebAPI.Models;

public partial class Course
{
    public Guid CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int Par { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
