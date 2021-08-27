using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBackendGame.Entities;
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
    }
}
