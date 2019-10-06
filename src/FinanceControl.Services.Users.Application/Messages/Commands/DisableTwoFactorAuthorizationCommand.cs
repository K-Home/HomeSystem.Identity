using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class DisableTwoFactorAuthorizationCommand : IAuthenticatedCommand
    {
        public Request Request { get; }
        public Guid UserId { get; }
        public bool DisableTwoFactorAuthentication { get; }

        [JsonConstructor]
        public DisableTwoFactorAuthorizationCommand(Guid userId, bool disableTwoFactorAuthentication)
        {
            Request = Request.New<DisableTwoFactorAuthorizationCommand>();
            UserId = userId;
            DisableTwoFactorAuthentication = disableTwoFactorAuthentication;
        }
    }
}