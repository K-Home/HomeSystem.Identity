using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class UnlockAccount : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public string UnlockUserId { get; }

        [JsonConstructor]
        public UnlockAccount(Request request, Guid userId, string unlockUserId)
        {
            Request = request;
            UserId = userId;
            UnlockUserId = unlockUserId;
        }
    }
}
