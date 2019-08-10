using HomeSystem.Services.Identity.Application.Messages.Commands;
using HomeSystem.Services.Identity.Infrastructure.MediatR.Bus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly IMediatRBus _mediatRBus;

        public AccountsController(IMediatRBus mediatRBus)
        {
            _mediatRBus = mediatRBus ?? throw new ArgumentNullException(nameof(mediatRBus));
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody]SignUpCommand command)
        {
            var result = await _mediatRBus.Send(command);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
