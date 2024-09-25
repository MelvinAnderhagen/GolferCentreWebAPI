using GolferCentreWebAPI.DTO.Course;
using GolferCentreWebAPI.DTO.Golfer;

namespace GolferCentreWebAPI.DTO.Tournament
{
    public class GetTournamentScoresDTO
    {
        public int Score { get; set; }
        public DateOnly RoundDate { get; set; }
        public List<GetGolferDTO> Golfers { get; set; }
        public GetCourseDTO Course { get; set; }
    }
}
