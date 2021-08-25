using Microsoft.AspNetCore.Identity;
using SimpleBackendGame.Entities;
using SimpleBackendGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Services
{
    public class UserService
    {
        private readonly GameDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(GameDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        public int RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Login = dto.Login
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.HashedPassword = hashedPassword;
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            return newUser.Id;
        }
        /*public string GenerateJwtToken(LoginUserDto dto)
        {
            var user = _dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == dto.Email);

            if (user is null)
            {
                return "Podany email lub hasło nie istnieje";
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return "Podano nieprawidlowe haslo lub email";
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),

            };

            //opcjonalne claimy
            if (user.DateOfBirth.HasValue)
            {
                claims.Add(
                    new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd"))
                );
            }
            if (!string.IsNullOrEmpty(user.Nationality))
            {
                claims.Add(
                    new Claim("Nationality", user.Nationality)
                );
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var datenow = DateTime.Now;

            var expires = DateTime.Now.AddMinutes(_authenticationSettings.JwtExpireDays);

            // chujstwo jebane do naprawienia

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }*/
    }
}
