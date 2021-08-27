using SimpleBackendGame.Entities;
using SimpleBackendGame.Models;
using System.Collections.Generic;

namespace SimpleBackendGame.Services
{
    public interface IMessageService
    {
        ICollection<MessageDto> GetAll();
        ICollection<MessageDto> GetUserMessages(int userId);
        bool RemoveMessage(int messageId);
        bool RemoveUserMessages(int userId);
        void SendMessage(SendMessageDto dto);
    }
}