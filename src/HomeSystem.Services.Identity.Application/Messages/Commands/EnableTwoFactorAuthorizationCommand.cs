using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
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
        public EnabledTwoFactorAuthorizationCommand(Request request, Guid userId, bool enableTwoFactorAuthorization)
        {
            Request = request;
            UserId = userId;
            EnableTwoFactorAuthorization = enableTwoFactorAuthorization;
        }
    }
}