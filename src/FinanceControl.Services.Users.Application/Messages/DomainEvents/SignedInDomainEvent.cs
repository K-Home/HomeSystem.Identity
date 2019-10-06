using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignedInDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; }
        public string Email { get; }
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