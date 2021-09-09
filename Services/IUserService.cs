using SimpleBackendGame.Entities;
using SimpleBackendGame.Models;
using System.Collections.Generic;

namespace SimpleBackendGame.Services
{
    public interface IUserService
    {
        bool ChangeRole(int userId, string role);
        bool DeleteUser(int userId);
        string GenerateJwtToken(LoginUserDto dto);
        ICollection<UserDto> GetAll();
        UserDto GetUser(int userId);
        bool MuteUser(int userId);
        int RegisterUser(RegisterUserDto dto);
    }
}