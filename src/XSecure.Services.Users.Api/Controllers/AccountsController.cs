using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XSecure.Services.Users.Application.Messages.Commands;
using XSecure.Services.Users.Infrastructure;
using XSecure.Services.Users.Infrastructure.MediatR.Bus;

namespace XSecure.Services.Users.Api.Controllers
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
