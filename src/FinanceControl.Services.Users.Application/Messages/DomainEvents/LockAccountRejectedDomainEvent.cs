using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class LockAccountRejectedDomainEvent : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public Guid LockedUserId { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public LockAccountRejectedDomainEvent(Guid requestId, Guid userId,
            Guid lockedUserId, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            LockedUserId = lockedUserId;
            Reason = reason;
            Code = code;
        }
    }
}