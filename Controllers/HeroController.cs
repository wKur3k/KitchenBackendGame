using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBackendGame.Entities;
using SimpleBackendGame.Models;
using SimpleBackendGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Controllers
{
    [Route("api/hero")]
    [ApiController]
    [Authorize]
    public class HeroController : ControllerBase
    {
        private readonly IHeroService _heroService;

        public HeroController(IHeroService heroService)
        {
            _heroService = heroService;
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public ActionResult CreateHero([FromQuery] string name)
        {
            var heroId = _heroService.CreateHero(name);
            if(heroId == 0)
            {
                return BadRequest();
            }
            return Ok(heroId);
        }
        
        [HttpPut]
        [Route("item")]
        [Authorize(Roles = "user")]
        public ActionResult AddItem([FromQuery] int itemId)
        {
            var isAdded = _heroService.AddItem(itemId);
            if (isAdded)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{heroId}")]
        public ActionResult<Hero> GetHero([FromRoute] int heroId)
        {
            var hero = _heroService.GetHero(heroId);
            if(hero is null)
            {
                return NotFound();
            }
            return Ok(hero);
        }

        [HttpGet]
        [Route("user/{userId}")]
        public ActionResult<HeroDto> GetUserHero([FromRoute] int userId)
        {
            var hero = _heroService.GetUserHero(userId);
            if (hero is null)
            {
                return NotFound();
            }

            return Ok(hero);
        }

        [HttpGet]
        public ActionResult<ICollection<Hero>> GetHeroes()
        {
            var heroes = _heroService.GetHeroes();
            if(heroes is null)
            {
                return NotFound();
            }
            return Ok(heroes);
        }

        [HttpDelete]
        [Route("{heroId}")]
        [Authorize(Roles = "admin, moderator")]

        public ActionResult DeleteHero([FromRoute] int heroId)
        {
            var isDelted = _heroService.DeleteHero(heroId);
            if (isDelted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("user/{userId}")]
        [Authorize(Roles = "admin, moderator")]
        public ActionResult DeleteUserHero([FromRoute] int userId)
        {
            var isDelted = _heroService.DeleteUserHero(userId);
            if (isDelted)
            {
                return NoContent();
            }
            return NotFound();
        }     
    }
}
