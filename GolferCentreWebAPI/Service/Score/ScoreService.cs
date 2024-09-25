
using GolferCentreWebAPI.DTO.Score;
using GolferCentreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GolferCentreWebAPI.Service.Score
{
    public class ScoreService : IScoreService
    {
        private readonly GolferGoContext _context;
        public ScoreService(GolferGoContext context)
        {
            _context = context;
        }

        public GetScoreDTO GetScore(Guid id)
        {
            try
            {
                // Fetch the score along with related entities (Golfer, Tournament, and Course)
                var score = _context.Scores
                    .Include(g => g.Golfer) // Include related Golfer
                    .Include(t => t.Tournament) // Include related Tournament
                    .Include(c => c.Course) // Include related Course
                    .Where(s => s.ScoreId == id) // Filter by ScoreId
                    .Select(score => new GetScoreDTO
                    {
                        ScoreId = score.ScoreId,
                        Score1 = score.Score1,
                        RoundDate = score.RoundDate,
                        Golfer = new GetGolferScoreDTO
                        {
                            Firstname = score.Golfer.FirstName,
                            Lastname = score.Golfer.LastName,
                            Handicap = score.Golfer.Handicap,
                            Country = score.Golfer.Country
                        },
                        Tournament = new GetTournamentScoreDTO
                        {
                            TournamentName = score.Tournament.TournamentName
                        },
                        Course = new GetCourseScoreDTO
                        {
                            CourseName = score.Course.CourseName,
                            Location = score.Course.Location,
                            Par = score.Course.Par
                        }
                    })
                    .FirstOrDefault();

                if (score == null)
                {
                    Console.WriteLine("Score Id not found.");
                    return null;
                }

                return score;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Get score method failed: {ex.Message}");
                return null;
            }
        }

        public ICollection<GetScoresDTO> GetScores()
        {
            try
            {
                // Fetch the score along with related entities (Golfer, Tournament, and Course)
                var scores = _context.Scores
                    .Include(s => s.Golfer)      // Include related Golfer(s)
                    .Include(s => s.Tournament)  // Include related Tournament
                    .Include(s => s.Course)      // Include related Course
                    .Select(score => new GetScoresDTO
                    {
                        ScoreId = score.ScoreId,
                        Score1 = score.Score1,
                        RoundDate = score.RoundDate,

                        // Populate the list of golfers (even though it's one golfer in this case)
                        Golfer = new List<GetGolferScoreDTO>
                        {
                    new GetGolferScoreDTO
                    {
                        Firstname = score.Golfer.FirstName,
                        Lastname = score.Golfer.LastName,
                        Handicap = score.Golfer.Handicap,
                        Country = score.Golfer.Country
                    }
                        },

                        // Map tournament details
                        Tournament = new GetTournamentScoreDTO
                        {
                            TournamentName = score.Tournament.TournamentName
                        },

                        // Map course details
                        Course = new GetCourseScoreDTO
                        {
                            CourseName = score.Course.CourseName,
                            Location = score.Course.Location,
                            Par = score.Course.Par
                        }
                    })
                    .ToList();

                // Check if any scores were found
                if (scores == null || scores.Count == 0)
                {
                    Console.WriteLine("No scores found.");
                    return new List<GetScoresDTO>(); // Return empty list if no scores found
                }

                return scores;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"GetScores method failed: {ex.Message}");
                return new List<GetScoresDTO>(); // Return empty list in case of error
            }
        }
    }
}
