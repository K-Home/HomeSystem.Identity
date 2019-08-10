using HomeSystem.Services.Identity.Application.Messages.Commands;
using HomeSystem.Services.Identity.Infrastructure.MediatR.Bus;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HomeSystem.Services.Identity.Controllers
{
    public class AccountsController : BaseController
    {
        public AccountsController(IMediatRBus mediatRBus)
            : base(mediatRBus)
        {
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
            => await SendAsync(command, command.Request.Id, "accounts");
    }
}
