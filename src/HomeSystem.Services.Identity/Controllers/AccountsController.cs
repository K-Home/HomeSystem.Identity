using HomeSystem.Services.Identity.Application.Messages.Commands;
using HomeSystem.Services.Identity.Infrastructure.MediatR.Bus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Infrastructure;

namespace HomeSystem.Services.Identity.Controllers
{
    public class AccountsController : BaseController
    {
        public AccountsController(IMediatRBus mediatRBus, AppOptions settings)
            : base(mediatRBus, settings)
        {
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
            => await SendAsync(command);
    }
}
