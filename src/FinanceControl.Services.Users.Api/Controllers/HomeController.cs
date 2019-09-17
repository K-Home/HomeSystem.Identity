using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Services.Users.Api.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Finance Control Users Service");
        }
    }
}