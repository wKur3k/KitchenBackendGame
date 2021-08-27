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
        public ICollection<MessageDto> GetUserMessages(int userId)
        {
            var messages = _dbContext
                .Messages
                .Include(m => m.User)
                .Where(m => m.UserId == userId)
                .OrderBy(m => m.SentDate)
                .ToList();
            if (!messages.Any())
            {
                return null;
            }
            var result = _mapper.Map<List<MessageDto>>(messages);
            return result;
        }

        public bool RemoveUserMessages(int userId)
        {
            var messages = _dbContext
                .Messages
                .Where(m => m.UserId == userId);
            if (!messages.Any())
            {
                return false;
            }
            messages.ToList().ForEach(c => c.MessageContent = "Removed by moderator");
            _dbContext.SaveChanges();
            return true;
        }

        public bool RemoveMessage(int messageId)
        {
            var message = _dbContext
                .Messages
                .FirstOrDefault(m => m.Id == messageId);
            if (message is null)
            {
                return false;
            }
            message.MessageContent = "Removed by moderator";
            _dbContext.SaveChanges();
            return true;
        }
    }
}
