using System;
using System.Runtime.Serialization;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class LockAccount : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public string LockUserId { get; }

        [JsonConstructor]
        public LockAccount(Request request, Guid userId, string lockUserId)
        {
            Request = request;
            UserId = userId;
            LockUserId = lockUserId;
        }
    }
}
