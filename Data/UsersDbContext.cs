

using Microsoft.EntityFrameworkCore;
using UsersApi.Models;

namespace UsersApi.Data
{
    public class UsersDbContext:DbContext 
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {

        }
         
        public DbSet<User> Users { get; set; }
    }
}
