using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class EnabledTwoFactorAuthorizationCommand : IAuthenticatedCommand
    {
        [DataMember] public Request Request { get; }

        [DataMember] public Guid UserId { get; }

        [DataMember] public bool EnableTwoFactorAuthorization { get; }

        [JsonConstructor]
        public EnabledTwoFactorAuthorizationCommand(Guid userId, bool enableTwoFactorAuthorization)
        {
            Request = Request.New<SignUpCommand>();
            UserId = userId;
            EnableTwoFactorAuthorization = enableTwoFactorAuthorization;
        }
    }
}