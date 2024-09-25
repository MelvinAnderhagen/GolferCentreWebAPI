using GolferCentreWebAPI.DTO.Golfer;

namespace GolferCentreWebAPI.Service.Golfer
{
    public interface IGolferService
    {
        bool CreateGolfer(CreateGolferDTO createGolferDTO);
        bool DeleteGolfer(Guid id);
        ICollection<GetGolferDTO> GetAllGolfers();
        GetGolferDTO GetGolfer(Guid id);
        bool UpdateGolfer(UpdateGolferDTO updateGolferDTO);
    }
}
