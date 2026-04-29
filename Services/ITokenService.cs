using UsersApi.Models;

namespace UsersApi.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}