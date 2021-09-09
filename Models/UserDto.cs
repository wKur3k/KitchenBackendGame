using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string login { get; set; }
        public bool CanUseChat { get; set; }
        public string Role { get; set; }
        public int? HeroId { get; set; }

    }
}
