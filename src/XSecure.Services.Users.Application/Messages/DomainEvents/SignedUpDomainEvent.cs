using System;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.DomainEvents
{
    public class SignedUpDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; }
        public Resource Resource { get; }
        public string Role { get; }
        public string State { get; }

        public SignedUpDomainEvent(Guid requestId, Guid userId, string message, 
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
