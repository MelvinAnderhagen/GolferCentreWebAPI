using GolferCentreWebAPI.DTO.Users;
using GolferCentreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GolferCentreWebAPI.Service.Users
{
    public class UserService : IUserService
    {

        private readonly GolferGoContext _context;

        public UserService(GolferGoContext context)
        {
            _context = context;
        }

        // Get all users
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(user => new UserDTO
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt,
                    LastLoginAt = user.LastLoginAt,
                    IsActive = user.IsActive
                }).ToListAsync();
        }

        // Get user by ID
        public async Task<UserDTO> GetUserByIdAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return null;

            return new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                LastLoginAt = user.LastLoginAt,
                IsActive = user.IsActive
            };
        }

        // Login method
        public async Task<UserResponseDTO> LoginAsync(UserLoginDTO userLoginDTO)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == userLoginDTO.Username);

            if (user == null || !VerifyPassword(userLoginDTO.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            // You can replace this with real token generation logic (e.g., JWT).
            string token = "fake-jwt-token";

            return new UserResponseDTO
            {
                Username = user.Username,
                Role = user.Role,
                Token = token
            };
        }

        // Logout (dummy method for now, can be expanded to invalidate tokens, etc.)
        public async Task LogoutAsync()
        {
            // Logout logic here (if needed)
            await Task.CompletedTask;
        }

        // Password verification (simplified)
        private bool VerifyPassword(string password, string storedPasswordHash)
        {
            // Normally, you'd hash the password and compare hashes.
            return password == storedPasswordHash;
        }
    }
}
