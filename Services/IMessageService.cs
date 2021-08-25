using SimpleBackendGame.Entities;
using SimpleBackendGame.Models;
using System.Collections.Generic;

namespace SimpleBackendGame.Services
{
    public interface IMessageService
    {
        ICollection<MessageDto> GetAll();
        void SendMessage(SendMessageDto dto);
    }
}