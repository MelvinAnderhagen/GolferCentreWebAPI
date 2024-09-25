using GolferCentreWebAPI.DTO.Course;
using GolferCentreWebAPI.DTO.Golfer;
using GolferCentreWebAPI.DTO.Tournament;
using GolferCentreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GolferCentreWebAPI.Service.Tournament
{
    public class TournamentService : ITournamentService
    {
        private readonly GolferGoContext _context;
        public TournamentService(GolferGoContext context)
        {
            _context = context;
        }

        public List<GetTournamentsDTO> GetTournaments()
        {
            try
            {
                var tournaments = _context.Tournaments
                    .Select(x => new GetTournamentsDTO
                    {
                        TournamentId = x.TournamentId,
                        TournamentName = x.TournamentName,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate
                    }).ToList();

                if (tournaments == null)
                {
                    return null;
                }

                return tournaments;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message: " + ex.Message);
                return null;
            }
        }

        public GetTournamentDTO GetTournament(Guid id)
        {
            try
            {
                // Fetch the tournament with related scores, golfers, and courses
                var tournament = _context.Tournaments
                    .Include(t => t.Scores) // Include related scores
                        .ThenInclude(s => s.Golfer) // Include related golfers for each score
                    .Include(t => t.Scores)
                        .ThenInclude(s => s.Course) // Include related course for each score
                    .Where(t => t.TournamentId == id) // Filter by tournament ID
                    .Select(t => new GetTournamentDTO
                    {
                        TournamentId = t.TournamentId,
                        TournamentName = t.TournamentName,
                        StartDate = t.StartDate,
                        EndDate = t.EndDate,
                        Scores = t.Scores.Select(s => new GetTournamentScoresDTO
                        {
                            Score = s.Score1,
                            RoundDate = s.RoundDate,
                            Golfers = new List<GetGolferDTO>
                            {
                        new GetGolferDTO
                        {
                            GolferId = s.Golfer.GolferId,
                            Firstname = s.Golfer.FirstName,
                            Lastname = s.Golfer.LastName,
                            Handicap = s.Golfer.Handicap
                        }
                            },
                            Course = new GetCourseDTO
                            {
                                CourseId = s.Course.CourseId,
                                CourseName = s.Course.CourseName,
                                Location = s.Course.Location,
                                Par = s.Course.Par
                            }
                        }).ToList() // Convert the related scores to a list
                    })
                    .FirstOrDefault(t => t.TournamentId == id);

                // Check if the tournament was found
                if (tournament == null)
                {
                    Console.WriteLine($"Tournament with ID {id} not found.");
                    return null; // Return null if not found
                }

                return tournament; // Return the populated DTO
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while fetching tournament: " + ex.Message);
                return null; // Return null in case of error
            }
        }
    }
}
