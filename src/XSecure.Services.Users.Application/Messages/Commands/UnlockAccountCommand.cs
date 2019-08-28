using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.Commands
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
