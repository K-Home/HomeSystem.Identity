using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Messages.Commands;
using FinanceControl.Services.Users.Infrastructure;
using FinanceControl.Services.Users.Infrastructure.MediatR.Bus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Services.Users.Api.Controllers
{
    public class AccountsController : BaseController
    {
        public AccountsController(IMediatRBus mediatRBus, AppOptions settings)
            : base(mediatRBus, settings)
        {
        }

        [HttpPost]
        [Route("activate")]
        public async Task<IActionResult> Post([FromBody] ActivateAccountCommand command)
        {
            return await SendAsync(command, "accounts/activate");
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("sign-up")]
        public async Task<IActionResult> Post([FromBody] SignUpCommand command)
        {
            return await SendAsync(command, "accounts/sign-up");
        }
    }
}