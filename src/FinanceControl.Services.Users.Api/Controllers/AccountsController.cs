using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Messages.Commands;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Infrastructure;
using FinanceControl.Services.Users.Infrastructure.MediatR.Bus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Services.Users.Api.Controllers
{
    public class AccountsController : BaseController
    {
        public AccountsController(IMediatRBus mediatRBus, IAuthenticationService authenticationService,
            AppOptions settings) : base(mediatRBus, authenticationService, settings)
        {
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("activate")]
        public async Task<IActionResult> Post([FromBody] ActivateAccountCommand command)
        {
            return await HandleRequestAsync(command);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-up")]
        public async Task<IActionResult> Post([FromBody] SignUpCommand command)
        {
            return await HandleRequestAsync(command);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("resend-activate-email")]
        public async Task<IActionResult> Post([FromBody] SendActivateAccountMessageCommand command)
        {
            return await HandleRequestAsync(command);
        }
    }
}