using Microsoft.AspNetCore.Mvc;
using UsersApi.Models;
using System.Collections.Generic;
using UsersApi.Data;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            _logger.LogInformation("Fetching all users");
            var users = await _context.Users.ToListAsync();
            _logger.LogInformation("Returned {Count} users", users.Count);
            return users;
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created user with Id {UserId}", user.Id);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            _logger.LogInformation("Fetching user {UserId}", id);
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found", id);
                return NotFound();
            }

            return user;
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
        public async Task<ActionResult<User>> EditUser(int id, User incomeUser)
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
            return Ok(existing);
        }
    }
}
