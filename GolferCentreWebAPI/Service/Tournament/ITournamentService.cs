using GolferCentreWebAPI.DTO.Tournament;

namespace GolferCentreWebAPI.Service.Tournament
{
    public interface ITournamentService
    {
        List<GetTournamentsDTO> GetTournaments();
        GetTournamentDTO GetTournament(Guid id);
    }
}
