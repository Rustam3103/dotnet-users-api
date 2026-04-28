using Microsoft.AspNetCore.Mvc;
using UsersApi.DTOs;
using UsersApi.Services;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();

            return users;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> AddUser(CreateUserDto user)
        {
            var newUser = await _userService.AddUserAsync(user);

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);

            if (deleted == false)
            {
                _logger.LogWarning("Delete failed: user {UserId} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Deleted user {UserId}", id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> EditUser(int id, UpdateUserDto incomeUser)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, incomeUser);
            if (updatedUser == null)
            {
                _logger.LogWarning("Update failed: user {UserId} not found", id);
                return NotFound("Not found");
            }

            _logger.LogInformation("Updated user {UserId}", id);

            return Ok(updatedUser);
        }
    }
}
