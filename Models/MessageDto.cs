using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Models
{
    public class MessageDto
    {
        public string MessageContent { get; set; }
        public DateTime SentDate { get; set; }
        public string Login { get; set; }
    }
}
