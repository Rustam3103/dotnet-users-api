using System.ComponentModel.DataAnnotations;

namespace UsersApi.DTOs
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name {get; set;} = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email {get; set;} = string.Empty;
    }
}