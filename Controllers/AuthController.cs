using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersApi.Data;
using UsersApi.DTOs;
using UsersApi.Models;
using UsersApi.Services;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UsersDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;
        public AuthController(
            UsersDbContext context,
            IPasswordHasher<User> passwordHasher,
            ITokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (emailExists)
                return BadRequest("Email already exists");
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User registered" });
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user is null)
                return Unauthorized("Invalid credentials");
            var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (verifyResult == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid credentials");
            var token = _tokenService.CreateToken(user);
            return Ok(new
            {
                token,
                user = new { user.Id, user.Name, user.Email }
            });
        }
    }
}