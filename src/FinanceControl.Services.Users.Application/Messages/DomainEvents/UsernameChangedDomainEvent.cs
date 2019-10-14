using System;
using FinanceControl.Services.Users.Domain.Types.Events;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class UsernameChangedDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Name { get; }

        [JsonConstructor]
        public UsernameChangedDomainEvent(Guid requestId,
            Guid userId, string name)
        {
            RequestId = requestId;
            UserId = userId;
            Name = name;
        }
    }
}