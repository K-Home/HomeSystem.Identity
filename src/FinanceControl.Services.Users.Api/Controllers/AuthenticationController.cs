using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Messages.Commands;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Infrastructure;
using FinanceControl.Services.Users.Infrastructure.MediatR.Bus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Services.Users.Api.Controllers
{
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IMediatRBus mediatRBus, IAuthenticationService authenticationService,
            AppOptions settings) : base(mediatRBus, authenticationService, settings)
        {
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-in")]
        public async Task<IActionResult> Post([FromBody]SignInCommand command)
        {
            return await HandleRequestAsync(command);
        }

        [HttpPost]
        [Route("sign-out")]
        public async Task<IActionResult> Post([FromBody]SignOutCommand command)
        {
            return await HandleRequestWithToken(command);
        }
    }
}