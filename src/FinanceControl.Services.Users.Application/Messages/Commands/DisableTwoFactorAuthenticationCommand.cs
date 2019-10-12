using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class DisableTwoFactorAuthenticationCommand : IAuthenticatedCommand
    {
        public Request Request { get; }
        public Guid UserId { get; }

        [JsonConstructor]
        public DisableTwoFactorAuthenticationCommand(Guid userId, bool disableTwoFactorAuthentication)
        {
            Request = Request.New<DisableTwoFactorAuthenticationCommand>();
            UserId = userId;
        }
    }
}