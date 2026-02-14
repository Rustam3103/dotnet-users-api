using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Models;
using System.Collections.Generic;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>();
       

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(users);
        }

        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            if (user == null)
            {
                return BadRequest("400");
            }
            else
            {
                users.Add(user);
                return Ok(user);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var selectedUser = users.FirstOrDefault(u => u.Id == id);
            if (selectedUser == null)
            {
                return NotFound("Not Found");
            }

            return Ok(selectedUser);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if(user == null)
            {
                return NotFound("Not found");
            }

            users.Remove(user);
            return Ok("Deleted successfully");
        }

        [HttpPut("{id}")]
        public ActionResult<string> EditUser(int id, User incomeUser)
        {
            var isUserExists = users.FirstOrDefault(user => user.Id == id);

            if(isUserExists == null)
            {
               return NotFound("Not found");
            }

            isUserExists.Email = incomeUser.Email;
            isUserExists.Name = incomeUser.Name;

            return Ok(isUserExists);
        }
       
    }
}
