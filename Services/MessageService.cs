using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleBackendGame.Entities;
using SimpleBackendGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Services
{
    public class MessageService : IMessageService
    {
        private readonly GameDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public MessageService(GameDbContext dbContext, IUserContextService userContextService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public void SendMessage(SendMessageDto dto)
        {
            var newMessage = new Message()
            {
                MessageContent = dto.Text,
                UserId = (int)_userContextService.GetUserId
            };
            _dbContext.Messages.Add(newMessage);
            _dbContext.SaveChanges();
        }

        public ICollection<MessageDto> GetAll()
        {
            var messages = _dbContext
                .Messages
                .Include(m => m.User)
                .OrderBy(m => m.SentDate)
                .ToList();
            if (!messages.Any())
            {
                return null;
            }
            var result = _mapper.Map<List<MessageDto>>(messages);
            return result;
        }
    }
}
