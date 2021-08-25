using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Entities
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ActionLeft { get; set; } = 5;
        public byte[] Art { get; set; }
        public int Gold { get; set; } = 100;
        public ICollection<Item> Items { get; set; }
        public virtual User User { get; set; }

    }
}
