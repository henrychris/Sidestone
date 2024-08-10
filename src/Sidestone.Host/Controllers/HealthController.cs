using HenryUtils.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sidestone.Host.Controllers
{
    [Authorize]
    public class HealthController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello World!");
        }
    }
}
