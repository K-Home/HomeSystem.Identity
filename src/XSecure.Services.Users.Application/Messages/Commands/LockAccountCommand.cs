using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.Commands
{
    public class LockAccountCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public string LockUserId { get; }

        [JsonConstructor]
        public LockAccountCommand(Guid userId, string lockUserId)
        {
            Request = Request.New<SignUpCommand>();
            UserId = userId;
            LockUserId = lockUserId;
        }
    }
}
