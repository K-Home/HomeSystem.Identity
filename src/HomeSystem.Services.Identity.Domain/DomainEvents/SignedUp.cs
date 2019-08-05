using HomeSystem.Services.Domain.DomainEvents.Base;
using System;

namespace HomeSystem.Services.Identity.Domain.DomainEvents
{
    public class SignedUp : IDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Role { get; }
        public string State { get; }

        public SignedUp(Guid requestId, Guid userId, string role, string state)
        {
            RequestId = requestId;
            UserId = userId;
            Role = role;
            State = state;
        }
    }
}
