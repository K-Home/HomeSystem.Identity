using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
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
        public LockAccountCommand(Request request, Guid userId, string lockUserId)
        {
            Request = request;
            UserId = userId;
            LockUserId = lockUserId;
        }
    }
}
