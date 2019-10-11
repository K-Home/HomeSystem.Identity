using System;
using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class AvatarRemovedDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }

        public AvatarRemovedDomainEvent(Guid requestId, Guid userId)
        {
            RequestId = requestId;
            UserId = userId;
        }
    }
}