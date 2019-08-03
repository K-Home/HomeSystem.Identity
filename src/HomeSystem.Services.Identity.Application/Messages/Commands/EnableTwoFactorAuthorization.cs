using System;
using System.Runtime.Serialization;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class EnabledTwoFactorAuthorization : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public bool EnableTwoFactorAuthorization { get; }

        [JsonConstructor]
        public EnabledTwoFactorAuthorization(Request request, Guid userId, bool enableTwoFactorAuthorization)
        {
            Request = request;
            UserId = userId;
            EnableTwoFactorAuthorization = enableTwoFactorAuthorization;
        }
    }
}