using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class EnableTwoFactorAuthenticationCommand : IAuthenticatedCommand
    {
        public Request Request { get; }
        public Guid UserId { get; }
        public bool EnableTwoFactorAuthorization { get; }

        [JsonConstructor]
        public EnableTwoFactorAuthenticationCommand(Guid userId, bool enableTwoFactorAuthorization)
        {
            Request = Request.New<EnableTwoFactorAuthenticationCommand>();
            UserId = userId;
            EnableTwoFactorAuthorization = enableTwoFactorAuthorization;
        }
    }
}