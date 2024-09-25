namespace GolferCentreWebAPI.DTO.Tournament
{
    public class GetTournamentsDTO
    {
        public Guid TournamentId { get; set; }

        public string TournamentName { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }
}
