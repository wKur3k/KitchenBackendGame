using SimpleBackendGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Services
{
    public class QuestService : IQuestService
    {
        private readonly GameDbContext _dbContext;
        private readonly IUserContextService _userContextService;

        public QuestService(GameDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        public bool GoQuest(int questId)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == _userContextService.GetUserId);
            var hero = user.Hero;
            var quest = _dbContext
                .Quests
                .FirstOrDefault(q => q.Id == questId);
            if (hero.ActionLeft <= 0 || hero is null || quest is null || user is null)
            {
                return false;
            }
            Fight(hero, quest);
            return true;
        }
        private void Fight(Hero hero, Quest quest)
        {
            hero.ActionLeft -= 1;
            Random _random = new Random();
            var enemy = quest.Enemy;
            int enemyStart = _random.Next(1, 100) + enemy.Speed;
            int heroStart = _random.Next(1, 100) + hero.Speed;
            int heroAtk;
            int enemyAtk;
            int heroHp = hero.Hp;
            int enemyHp = enemy.Hp;
            bool win;
            if (heroStart > enemyStart)
            {
                while (true)
                {
                    if (hero.Crit > _random.Next(1, 100))
                    {
                        heroAtk = hero.Atk * 2;
                    }
                    else
                    {
                        heroAtk = hero.Atk;
                    }
                    if (enemy.Crit > _random.Next(1, 100))
                    {
                        enemyAtk = hero.Atk * 2;
                    }
                    else
                    {
                        enemyAtk = enemy.Atk;
                    }
                    enemyHp = enemyHp - heroAtk + enemy.Def;
                    if (enemy.Hp <= 0)
                    {
                        win = true;
                        break;
                    }
                    heroHp = heroHp - enemyAtk + hero.Def;
                    if (hero.Hp <= 0)
                    {
                        win = false;
                        break;
                    }
                }
            }
            else
            {
                while (true)
                {
                    if (hero.Crit > _random.Next(1, 100))
                    {
                        heroAtk = hero.Atk * 2;
                    }
                    else
                    {
                        heroAtk = hero.Atk;
                    }
                    if (enemy.Crit > _random.Next(1, 100))
                    {
                        enemyAtk = hero.Atk * 2;
                    }
                    else
                    {
                        enemyAtk = enemy.Atk;
                    }
                    heroHp = heroHp - enemyAtk + hero.Def;
                    if (hero.Hp <= 0)
                    {
                        win = false;
                        break;
                    }
                    enemyHp = enemyHp - heroAtk + enemy.Def;
                    if (enemy.Hp <= 0)
                    {
                        win = true;
                        break;
                    }
                }
            }
            if (!win)
            {
                return;
            }
            hero.Gold += quest.GoldReward;
            _dbContext.SaveChanges();
        }
    }
}
