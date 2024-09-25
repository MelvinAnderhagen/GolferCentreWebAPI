namespace GolferCentreWebAPI.DTO.Score
{
    public class GetGolferScoreDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public decimal Handicap { get; set; }
        public string Country { get; set; }
    }
}
