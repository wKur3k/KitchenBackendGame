using SimpleBackendGame.Entities;
using System.Collections.Generic;

namespace SimpleBackendGame.Services
{
    public interface IHeroService
    {
        bool AddItem(int itemId);
        int CreateHero(string name);
        bool DeleteHero(int heroId);
        bool DeleteUserHero(int userId);
        Hero GetHero(int heroId);
        ICollection<Hero> GetHeroes();
        Hero GetUserHero(int userId);
        bool GoQuest(int questId);
    }
}