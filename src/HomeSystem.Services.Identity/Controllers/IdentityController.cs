using System.Threading.Tasks;
using HomeSystem.Services.Identity.Messages.Commands;
using HomeSystem.Services.Identity.Services;
using KShared.Authentication.Attributes;
using KShared.Configuration.Extensions;
using KShared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace HomeSystem.Services.Identity.Controllers
{
    [Route("")]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;
        private readonly IRefreshTokenService _refreshTokenService;

        public IdentityController(IIdentityService identityService,
            IRefreshTokenService refreshTokenService,
            IDispatcher dispatcher) : base(dispatcher)
        {
            _identityService = identityService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpGet("me")]
        [JwtAuth]
        public IActionResult Get() => Content($"Your id: '{UserId:N}'.");

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUp command)
        {
            command.BindId(c => c.Id);
            await _identityService.SignUpAsync(command.Id, command.FirstName, 
                command.LastName, command.Email, command.Password, command.Role);

            return NoContent();
        }
    }
}