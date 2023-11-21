using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sudnica_API.Utility;

namespace Sudnica_API.Controllers
{
    [Route("api/authTest")]
    [ApiController]
    public class AuthTestController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<string>> Autentifikacija()
        {
            return "Autentifikovani ste!";
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<ActionResult<string>> Autorizacija(int vrednost)
        {
            return "Autorizovani ste sa ulogom Administratora!";
        }
    }
}
