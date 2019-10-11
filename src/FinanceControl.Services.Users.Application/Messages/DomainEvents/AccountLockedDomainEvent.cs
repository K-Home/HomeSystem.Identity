using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class AccountLockedDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public Guid LockedUserId { get; }

        [JsonConstructor]
        public AccountLockedDomainEvent(Guid requestId, Guid userId, Guid lockedUserId)
        {
            RequestId = requestId;
            UserId = userId;
            LockedUserId = lockedUserId;
        }
    }
}