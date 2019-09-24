using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignedInDomainEvent : IDomainEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public string UserId { get; }

        [DataMember]
        public string Email { get; }

        [DataMember]
        public string Name { get; }

        [JsonConstructor]
        public SignedInDomainEvent(Guid requestId, string userId,
            string email, string name)
        {
            RequestId = requestId;
            UserId = userId;
            Email = email;
            Name = name;
        }
    }
}