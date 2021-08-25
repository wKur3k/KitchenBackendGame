using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public ActionResult RegisterUser(RegisterUserDto dto)
        {
            int userId = _userService.RegisterUser(dto);
            return Created($"{userId}", null);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult LoginUser(LoginUserDto dto)
        {
            string token = _userService.GenerateJwtToken(dto);
            return Ok(token);
        }

        [HttpGet]
        [Route("{userId}")]
        [Authorize(Roles = "admin, moderator")]
        public ActionResult<User> GetUser([FromRoute] int userId)
        {
            var user = _userService.GetUser(userId);
            if (user is null)
            {
                return NotFound($"User with Id: {userId}, Not Found");
            }
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "admin, moderator")]
        public ActionResult<ICollection<User>> GetAll()
        {
            var users = _userService.GetAll();
            if (users is null)
            {
                return NotFound("No users found");
            }
            return Ok(users);
        }

        [HttpDelete]
        [Route("{userId}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteUser([FromRoute] int userId)
        {
            bool isDelted = _userService.DeleteUser(userId);
            if (isDelted)
            {
                return NoContent();
            }
            return NotFound("User not found");
        }

        [HttpPut]
        [Route("{userId}/mute")]
        [Authorize(Roles = "moderator")]
        public ActionResult MuteUser([FromRoute] int userId)
        {
            bool isMuted = _userService.MuteUser(userId);
            if (isMuted)
            {
                return Ok();
            }
            return NotFound("User not found");
        }

        [HttpPut]
        [Route("{userId}/role")]
        [Authorize(Roles = "admin")]
        public ActionResult ChangeUserRole([FromRoute] int userId, [FromQuery] string role)
        {
            bool hasChanged = _userService.ChangeRole(userId, role);
            if (hasChanged)
            {
                return Ok();
            }
            return NotFound("User not found");
        }

        [HttpGet]
        [Route("{userId}/message")]
        [Authorize(Roles = "moderator")]
        public ActionResult<ICollection<MessageDto>> GetUserMessages([FromRoute] int userId)
        {
            var messages = _userService.GetUserMessages(userId);
            if(messages is null)
            {
                return NotFound();
            }
            return Ok(messages);
        }
    }
}
