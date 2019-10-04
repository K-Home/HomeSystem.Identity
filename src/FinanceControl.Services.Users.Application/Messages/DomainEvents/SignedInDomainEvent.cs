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
        public Guid UserId { get; }

        [DataMember]
        public string Message { get; }

        [DataMember]
        public string Email { get; }

        [DataMember]
        public string Name { get; }

        [JsonConstructor]
        public SignedInDomainEvent(Guid requestId, Guid userId,
            string message, string email, string name)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Email = email;
            Name = name;
        }
    }
}