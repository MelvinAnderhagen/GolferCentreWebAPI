using GolferCentreWebAPI.DTO.Users;

namespace GolferCentreWebAPI.Service.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(Guid userId);
        Task<UserResponseDTO> LoginAsync(UserLoginDTO userLoginDTO);
        Task LogoutAsync();
    }
}
