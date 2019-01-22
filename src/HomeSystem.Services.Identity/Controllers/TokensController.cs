using System.Threading.Tasks;
using HomeSystem.Services.Identity.Controllers;
using HomeSystem.Services.Identity.Messages.Commands;
using HomeSystem.Services.Identity.Services;
using KShared.Authentication.Attributes;
using KShared.Authentication.Services;
using KShared.Configuration.Extensions;
using KShared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeSystem.Services.Identity
{
    [Route("")]
    [ApiController]
    [JwtAuth]
    public class TokensController : BaseController
    {
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public TokensController(IAccessTokenService accessTokenService,
            IRefreshTokenService refreshTokenService,
            IDispatcher dispatcher) : base(dispatcher)
        {
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
        }
        
        [HttpPost("access-tokens/{refreshToken}/refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshAccessToken(string refreshToken, RefreshAccessToken command)
            => Ok(await _refreshTokenService.CreateAccessTokenAsync(command.Bind(c => c.Token, refreshToken).Token));

        [HttpPost("access-tokens/revoke")]
        public async Task<IActionResult> RevokeAccessToken(RevokeAccessToken command)
        {
            await _accessTokenService.DeactivateCurrentAsync(
                command.Bind(c => c.UserId, UserId).UserId.ToString("N"));

            return NoContent();
        }

        [HttpPost("refresh-tokens/{refreshToken}/revoke")]
        public async Task<IActionResult> RevokeRefreshToken(string refreshToken, RevokeRefreshToken command)
        {
            await _refreshTokenService.RevokeAsync(command.Bind(c => c.Token, refreshToken).Token, 
                command.Bind(c => c.UserId, UserId).UserId);

            return NoContent();
        }
    }
}
