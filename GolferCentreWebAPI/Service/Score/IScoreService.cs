using GolferCentreWebAPI.DTO.Score;

namespace GolferCentreWebAPI.Service.Score
{
    public interface IScoreService
    {
        ICollection<GetScoresDTO> GetScores();
        GetScoreDTO GetScore(Guid id);
    }
}
