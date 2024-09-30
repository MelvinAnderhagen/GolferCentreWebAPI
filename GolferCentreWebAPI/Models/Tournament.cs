using System;
using System.Collections.Generic;

namespace GolferCentreWebAPI.Models;

public partial class Tournament
{
    public Guid TournamentId { get; set; }

    public string TournamentName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
