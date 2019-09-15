using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
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
        public DisableTwoFactorAuthorizationCommand(Guid userId, bool disableTwoFactorAuthentication)
        {
            Request = Request.New<SignUpCommand>();
            UserId = userId;
            DisableTwoFactorAuthentication = disableTwoFactorAuthentication;
        }
    }
}
