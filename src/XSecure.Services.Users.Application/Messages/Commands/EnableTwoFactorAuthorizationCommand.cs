using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.Commands
{
    public class EnabledTwoFactorAuthorizationCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public bool EnableTwoFactorAuthorization { get; }

        [JsonConstructor]
        public EnabledTwoFactorAuthorizationCommand(Guid userId, bool enableTwoFactorAuthorization)
        {
            Request = Request.New<SignUpCommand>();
            UserId = userId;
            EnableTwoFactorAuthorization = enableTwoFactorAuthorization;
        }
    }
}