using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleBackendGame.Entities;
using SimpleBackendGame.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBackendGame.Services
{
    public class UserService : IUserService
    {
        private readonly GameDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IMapper _mapper;

        public UserService(GameDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IMapper mapper)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _mapper = mapper;
        }
        public int RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Login = dto.Login
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.HashedPassword = hashedPassword;
            if (dto.Role == "admin")
            {
                newUser.Role = "admin";
            }
            if (dto.Role == "moderator")
            {
                newUser.Role = "moderator";
            }
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            return newUser.Id;
        }
        public string GenerateJwtToken(LoginUserDto dto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Login == dto.Login);

            if (user is null)
            {
                return "Login or Password is incorrect";
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return "Login or Password is incorrect";
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Login}"),
                new Claim(ClaimTypes.Role, $"{user.Role}"),

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var datenow = DateTime.Now;
            var expires = DateTime.Now.AddMinutes(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        public UserDto GetUser(int userId)
        {
            var user = _dbContext
                .Users
                .Include(u => u.Hero)
                .FirstOrDefault(u => u.Id == userId);
            if(user is null)
            {
                return null;
            }
            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public ICollection<UserDto> GetAll()
        {
            var users = _dbContext
                .Users
                .Include(u => u.Hero)
                .ToList();
            if (!users.Any())
            {
                return null;
            }
            var result = _mapper.Map<ICollection<UserDto>>(users);

            return result;
        }

        public bool DeleteUser(int userId)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == userId);
            if(user is null)
            {
                return false;
            }
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            return true;
        }

        public bool MuteUser(int userId)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == userId);
            if(user is null)
            {
                return false;
            }
            user.CanUseChat = false;
            _dbContext.SaveChanges();
            return true;
        }

        public bool ChangeRole(int userId, string role)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == userId);
            if (user is null)
            {
                return false;
            }
            user.Role = role;
            _dbContext.SaveChanges();
            return true;
        }

    }
}
