using System;
using System.Collections.Generic;

namespace GolferCentreWebAPI.Models;

public partial class Score
{
    public Guid ScoreId { get; set; }

    public Guid GolferId { get; set; }

    public Guid TournamentId { get; set; }

    public Guid CourseId { get; set; }

    public int Score1 { get; set; }

    public DateOnly RoundDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Golfer Golfer { get; set; } = null!;

    public virtual Tournament Tournament { get; set; } = null!;
}
