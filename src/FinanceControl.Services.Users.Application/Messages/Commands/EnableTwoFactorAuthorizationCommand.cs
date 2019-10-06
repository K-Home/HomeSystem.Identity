using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class EnabledTwoFactorAuthorizationCommand : IAuthenticatedCommand
    {
        public Request Request { get; }
        public Guid UserId { get; }
        public bool EnableTwoFactorAuthorization { get; }

        [JsonConstructor]
        public EnabledTwoFactorAuthorizationCommand(Guid userId, bool enableTwoFactorAuthorization)
        {
            Request = Request.New<EnabledTwoFactorAuthorizationCommand>();
            UserId = userId;
            EnableTwoFactorAuthorization = enableTwoFactorAuthorization;
        }
    }
}