using Microsoft.AspNetCore.Mvc;

namespace HomeSystem.Services.Identity.Controllers
{
    [Route("")]
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Get() => Ok("Home System Identity Service");
    }
}