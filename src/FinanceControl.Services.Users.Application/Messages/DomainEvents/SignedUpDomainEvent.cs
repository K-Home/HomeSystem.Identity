using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignedUpDomainEvent : IDomainEvent
    {
        [DataMember]
        public Guid RequestId { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public string Message { get; }
        
        [DataMember]
        public Resource Resource { get; }
        
        [DataMember]
        public string Role { get; }
        
        [DataMember]
        public string State { get; }

        [JsonConstructor]
        public SignedUpDomainEvent(Guid requestId, Guid userId, string message,
            Resource resource, string role, string state)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Resource = resource;
            Role = role;
            State = state;
        }
    }
}