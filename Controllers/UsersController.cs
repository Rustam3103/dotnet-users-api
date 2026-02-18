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

        public UsersController(UsersDbContext context)
        {
            _context = context;
        }
       

        [HttpGet]
        public async Task<ActionResult<List<User>>>  GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return user;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> EditUser(int id, User incomeUser)
        {
            var isUserExists = await _context.Users.FindAsync(id);

            if(isUserExists == null)
            {
               return NotFound("Not found");
            }

            isUserExists.Email = incomeUser.Email;
            isUserExists.Name = incomeUser.Name;

            await _context.SaveChangesAsync();

            return Ok(isUserExists);
        }
       
    }
}
