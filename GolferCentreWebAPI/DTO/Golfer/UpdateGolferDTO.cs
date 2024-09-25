namespace GolferCentreWebAPI.DTO.Golfer
{
    public class UpdateGolferDTO
    {

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public decimal Handicap { get; set; }

        public string Country { get; set; } = null!;
    }
}
