using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UsersApi.Data;
using UsersApi.DTOs;
using UsersApi.Models;

namespace UsersApi.Services
{
    public class UserService : IUserService
    {
        private readonly UsersDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(UsersDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            _logger.LogInformation("Fetching all users");
            var users = await _context.Users.ToListAsync();
            
            _logger.LogInformation("Returned {Count} users", users.Count);
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToList();
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            _logger.LogInformation("Fetching user {UserId}", id);
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found", id);
                return null;
            }

            _logger.LogInformation("Returned user {UserId}", id);
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<UserDto> AddUserAsync(CreateUserDto user)
        {
            var entity = new User
            {
                Name = user.Name,
                Email = user.Email
            };

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created user with Id {UserId}", entity.Id);

            return new UserDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email
            };

        }

        public async Task<bool> DeleteUserAsync(int id)
        {

            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted user {UserId}", id);

            return true;
        }

        public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser is null) return null;

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Email = existingUser.Email
            };
        }
    }
}