using System;
using FinanceControl.Services.Users.Domain.Types.Events;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignedInDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Email { get; }
        public string Name { get; }

        [JsonConstructor]
        public SignedInDomainEvent(Guid requestId, Guid userId,
            string email, string name)
        {
            RequestId = requestId;
            UserId = userId;
            Email = email;
            Name = name;
        }
    }
}