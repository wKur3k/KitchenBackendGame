using Microsoft.AspNetCore.Mvc;
using SimpleBackendGame.Entities;
using SimpleBackendGame.Models;
using SimpleBackendGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult RegisterUser(RegisterUserDto dto)
        {
            int userId = _userService.RegisterUser(dto);
            return Created($"{userId}", null);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult LoginUser(LoginUserDto dto)
        {
            string token = _userService.GenerateJwtToken(dto);
            return Ok(token);
        }
        //dlaModa+Admina
        [HttpGet]
        [Route("{userId}")]
        public ActionResult<User> GetUser([FromRoute] int userId)
        {
            var user = _userService.GetUser(userId);
            if (user is null)
            {
                return NotFound($"User with Id: {userId}, Not Found");
            }
            return Ok(user);
        }
        //dlaModa+Admina
        [HttpGet]
        public ActionResult<ICollection<User>> GetAll()
        {
            var users = _userService.GetAll();
            if (users is null)
            {
                return NotFound("No users found");
            }
            return Ok(users);
        }
        //dlaAdmina
        [HttpDelete]
        [Route("{userId}")]
        public ActionResult DeleteUser([FromRoute] int userId)
        {
            bool isDelted = _userService.DeleteUser(userId);
            if (isDelted)
            {
                return NoContent();
            }
            return NotFound("User not found");
        }
        //dlaModa
        [HttpPut]
        [Route("{userId}/mute")]
        public ActionResult MuteUser([FromRoute] int userId)
        {
            bool isMuted = _userService.MuteUser(userId);
            if (isMuted)
            {
                return Ok();
            }
            return NotFound("User not found");
        }
        //dlaAdmina
        [HttpPut]
        [Route("{userId}/role")]
        public ActionResult ChangeUserRole([FromRoute] int userId, [FromQuery] string role)
        {
            bool hasChanged = _userService.ChangeRole(userId, role);
            if (hasChanged)
            {
                return Ok();
            }
            return NotFound("User not found");
        }
    }
}
