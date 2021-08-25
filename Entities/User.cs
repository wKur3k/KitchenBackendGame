using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string HashedPassword { get; set; }
        public bool CanUseChat { get; set; } = true;
        public string Role { get; set; } = "user";
        public int HeroId { get; set; }
        public virtual Hero Hero { get; set; }
    }
}
