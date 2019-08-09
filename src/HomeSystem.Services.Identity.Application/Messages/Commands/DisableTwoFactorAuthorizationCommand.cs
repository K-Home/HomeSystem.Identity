using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class DisableTwoFactorAuthorizationCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public bool DisableTwoFactorAuthentication { get; }

        [JsonConstructor]
        public DisableTwoFactorAuthorizationCommand(Request request, Guid userId, bool disableTwoFactorAuthentication)
        {
            Request = request;
            UserId = userId;
            DisableTwoFactorAuthentication = disableTwoFactorAuthentication;
        }
    }
}
