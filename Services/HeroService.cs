using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleBackendGame.Entities;
using SimpleBackendGame.Models;
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
        private readonly IMapper _mapper;

        public HeroService(GameDbContext dbContext, IUserContextService userContextService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _mapper = mapper;
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
        public HeroDto GetUserHero(int userId)
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
            var result = _mapper.Map<HeroDto>(hero);
            return result;
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
    }
}
