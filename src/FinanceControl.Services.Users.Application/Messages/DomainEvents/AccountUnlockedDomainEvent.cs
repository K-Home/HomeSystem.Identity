using System;
using FinanceControl.Services.Users.Domain.Types.Events;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class AccountUnlockedDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public Guid LockedUserId { get; }

        [JsonConstructor]
        public AccountUnlockedDomainEvent(Guid requestId, Guid userId, Guid lockedUserId)
        {
            RequestId = requestId;
            UserId = userId;
            LockedUserId = lockedUserId;
        }
    }
}