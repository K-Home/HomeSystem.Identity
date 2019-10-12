using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class AccountDeletedDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public bool Soft { get; }

        [JsonConstructor]
        public AccountDeletedDomainEvent(Guid requestId, Guid userId, bool soft)
        {
            RequestId = requestId;
            UserId = userId;
            Soft = soft;
        }
    }
}