using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Entities
{
    public class Quest
    {
        public int Id { get; set; }
        public int GoldReward { get; set; }
        public int Map { get; set; }
        public int EnemyId { get; set; }
        public virtual Enemy Enemy { get; set; }
    }
}
