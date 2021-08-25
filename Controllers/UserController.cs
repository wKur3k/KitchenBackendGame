using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("register")]
        public ActionResult RegisterUser()
        {
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult LoginUser()
        {
            return Ok();
        }
    }
}
