using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Models
{
    public class HeroDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ActionLeft { get; set; }
        public int Gold { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Crit { get; set; }
        public int Speed { get; set; }
    }
}
