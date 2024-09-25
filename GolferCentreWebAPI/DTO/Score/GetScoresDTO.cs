namespace GolferCentreWebAPI.DTO.Score
{
    public class GetScoresDTO
    {
        public Guid ScoreId { get; set; }

        public int Score1 { get; set; }

        public DateOnly RoundDate { get; set; }
        public List<GetGolferScoreDTO> Golfer { get; set; }
        public GetTournamentScoreDTO Tournament { get; set; }
        public GetCourseScoreDTO Course { get; set; }
    }
}
