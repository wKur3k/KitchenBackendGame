using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Route("api/message")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public ActionResult SendMessage([FromBody] SendMessageDto dto)
        {
            _messageService.SendMessage(dto);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<ICollection<MessageDto>> GetAll()
        {
            var messages = _messageService.GetAll();
            if(messages is null)
            {
                return NotFound();
            }
            return Ok(messages);
        }

        [HttpPut]
        [Authorize(Roles = "moderator")]
        public ActionResult RemoveUserMessages()
        {
            return Ok();
        }
    }
}
