using HomeSystem.Services.Identity.Infrastructure.Messages;
using System;

namespace HomeSystem.Services.Identity.Application.Messages.DomainEvents
{
    public class SignedUp : IDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; }
        public Resource Resource { get; }
        public string Role { get; }
        public string State { get; }

        public SignedUp(Guid requestId, Guid userId, string message, 
            Resource resource, string role, string state)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Resource = resource;
            Role = role;
            State = state;
        }
    }
}
