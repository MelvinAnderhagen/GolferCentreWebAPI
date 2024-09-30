using System;
using System.Collections.Generic;

namespace GolferCentreWebAPI.Models;

public partial class Golfer
{
    public Guid GolferId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public decimal Handicap { get; set; }

    public string Country { get; set; } = null!;

    public string? GolferImage { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
