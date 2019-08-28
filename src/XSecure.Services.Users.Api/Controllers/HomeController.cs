using Microsoft.AspNetCore.Mvc;

namespace XSecure.Services.Users.Api.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Home System Identity Service");
    }
}