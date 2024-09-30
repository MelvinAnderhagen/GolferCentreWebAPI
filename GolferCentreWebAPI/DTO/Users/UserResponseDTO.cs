namespace GolferCentreWebAPI.DTO.Users
{
    public class UserResponseDTO
    {
        public string Username { get; set; }
        public string Token { get; set; } // JWT or any token-based authentication
        public string Role { get; set; }
    }
}
