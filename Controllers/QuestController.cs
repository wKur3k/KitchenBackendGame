using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBackendGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        private readonly IQuestService _questService;

        public QuestController(IQuestService questService)
        {
            _questService = questService;
        }

        [HttpPost]
        [Route("{questId}")]
        [Authorize(Roles = "user")]
        public ActionResult GoQuest([FromRoute] int questId)
        {
            var questCompleted = _questService.GoQuest(questId);
            if (questCompleted)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
