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
                Name = name
            };
            _dbContext
                .Heroes
                .Add(newHero);
            _dbContext.SaveChanges();
            user.HeroId = newHero.Id;
            _dbContext.SaveChanges();
            return newHero.Id;
        }
    }
}
