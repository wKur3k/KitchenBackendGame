using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageContent { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
