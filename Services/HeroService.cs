using Microsoft.EntityFrameworkCore;
using SimpleBackendGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Services
{
    public class HeroService : IHeroService
    {
        private readonly GameDbContext _dbContext;
        private readonly IUserContextService _userContextService;

        public HeroService(GameDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        public int CreateHero(string name)
        {
            var userId = (int)_userContextService.GetUserId;
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == userId);
            if(user is null || user.HeroId is not null)
            {
                return 0;
            }
            var newHero = new Hero()
            {
                Name = name,
                User = user
            };
            _dbContext
                .Heroes
                .Add(newHero);
            _dbContext.SaveChanges();
            return newHero.Id;
        }
        
        public bool AddItem(int itemId)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == _userContextService.GetUserId);
            var hero = user.Hero;
            var item = _dbContext
                .Items
                .FirstOrDefault(i => i.Id == itemId);
            if(hero is null || item is null || user is null)
            {
                return false;
            }
            if(hero.Gold < item.Price)
            {
                return false;
            }
            if (hero.Items.Contains(item))
            {
                return false;
            }
            var heroItem = hero.Items.FirstOrDefault(i => i.Slot == item.Slot);
            if(heroItem is null)
            {
                hero.Items.Add(item);
            }
            else
            {
                hero.Items.Remove(heroItem);
                hero.Items.Add(item);

            }
            switch (item.Slot)
            {
                case "helm":
                    hero.Crit = item.Stat;
                    break;
                case "chest":
                    hero.Def = item.Stat;
                    break;
                case "boots":
                    hero.Speed = item.Stat;
                    break;
                case "weapon":
                    hero.Atk = item.Stat;
                    break;
                default:
                    break;
            }
            hero.Gold = -item.Price;
            _dbContext.SaveChanges();
            return true;
        }
        public Hero GetHero(int heroId)
        {
            var hero = _dbContext
                .Heroes
                .FirstOrDefault(h => h.Id == heroId);
            if(hero is null)
            {
                return null;
            }
            return hero;
        }
        public Hero GetUserHero(int userId)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == userId);
            var hero = user.Hero;
            if (user is null)
            {
                return null;
            }
            if(hero is null)
            {
                return null;
            }
            return hero;
        }
        public ICollection<Hero> GetHeroes()
        {
            var heroes = _dbContext
                .Heroes
                .ToList();
            if (!heroes.Any())
            {
                return null;
            }
            return heroes;
        }
        public bool DeleteHero(int heroId)
        {
            var hero = _dbContext
                .Heroes
                .FirstOrDefault(h => h.Id == heroId);
            if (hero is null)
            {
                return false;
            }
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.HeroId == heroId);
            user.HeroId = null;
            _dbContext.Heroes.Remove(hero);
            _dbContext.SaveChanges();
            return true;
        }
        public bool DeleteUserHero(int userId)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == userId);
            var hero = user.Hero;
            if (hero is null || user is null)
            {
                return false;
            }
            user.HeroId = null;
            _dbContext.Heroes.Remove(hero);
            _dbContext.SaveChanges();
            return true;
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
            if(hero.ActionLeft <= 0 || hero is null || quest is null || user is null)
            {
                return false;
            }
            bool fight = Fight(hero, quest);
            return true;
        }
        private bool Fight(Hero hero, Quest quest)
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
                while(true)
                {
                    if(hero.Crit > _random.Next(1, 100))
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
                return false;
            }
            hero.Gold += quest.GoldReward;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
