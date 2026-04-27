using Microsoft.AspNetCore.Mvc;
using UsersApi.Models;
using System.Collections.Generic;
using UsersApi.Data;
using Microsoft.EntityFrameworkCore;
using UsersApi.DTOs;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersDbContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UsersDbContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            _logger.LogInformation("Fetching all users");
            var users = await _context.Users.ToListAsync();
            _logger.LogInformation("Returned {Count} users", users.Count);
            var result = users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToList();

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> AddUser(CreateUserDto user)
        {
            var entity = new User
            {
                Name = user.Name,
                Email = user.Email
            };

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created user with Id {UserId}", entity.Id);

            var result = new UserDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email
            };

            return CreatedAtAction(nameof(GetUser), new { id = entity.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            _logger.LogInformation("Fetching user {UserId}", id);
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found", id);
                return NotFound();
            }

            var result = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                _logger.LogWarning("Delete failed: user {UserId} not found", id);
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted user {UserId}", id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> EditUser(int id, UpdateUserDto incomeUser)
        {
            var existing = await _context.Users.FindAsync(id);

            if (existing == null)
            {
                _logger.LogWarning("Update failed: user {UserId} not found", id);
                return NotFound("Not found");
            }

            existing.Email = incomeUser.Email;
            existing.Name = incomeUser.Name;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated user {UserId}", id);
            var result = new UserDto
            {
                Id = existing.Id,
                Name = existing.Name,
                Email = existing.Email
            };

            return Ok(result);
        }
    }
}
