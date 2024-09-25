using GolferCentreWebAPI.Models;

namespace GolferCentreWebAPI.DTO.Golfer
{
    public class GetGolferDTO
    {
        public Guid GolferId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public decimal Handicap { get; set; }
        public string Country { get; set; }
    }
}
