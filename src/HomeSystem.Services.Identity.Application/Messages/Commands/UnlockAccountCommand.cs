using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class UnlockAccountCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public string UnlockUserId { get; }

        [JsonConstructor]
        public UnlockAccountCommand(Guid userId, string unlockUserId)
        {
            Request = Request.New<SignUpCommand>();
            UserId = userId;
            UnlockUserId = unlockUserId;
        }
    }
}
