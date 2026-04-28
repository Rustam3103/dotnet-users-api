using UsersApi.DTOs;

namespace UsersApi.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id); 
        Task<UserDto> AddUserAsync(CreateUserDto user);
        Task<bool> DeleteUserAsync(int id);
        Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto user);
    }
}