using HenryUtils.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Sidestone.Host.Controllers
{
    public class HealthController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello World!");
        }
    }
}
