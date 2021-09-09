using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slot { get; set; }
        public int Stat { get; set; }
        public string Art { get; set; }
        public int Price { get; set; }
        public ICollection<Hero> Heroes { get; set; }

    }
}
