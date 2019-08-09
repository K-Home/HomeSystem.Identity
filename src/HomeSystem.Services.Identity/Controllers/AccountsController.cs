using System;
using System.Threading;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Application.Messages.Commands;
using HomeSystem.Services.Identity.Infrastructure.Extensions;
using HomeSystem.Services.Identity.Infrastructure.MediatR.Bus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeSystem.Services.Identity.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly Logger<AccountsController> _logger;
        private readonly IMediatRBus _mediatRBus;

        public AccountsController(Logger<AccountsController> logger, IMediatRBus mediatRBus)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediatRBus = mediatRBus ?? throw new ArgumentNullException(nameof(logger));
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
