using System;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.DomainEvents
{
    public class SignedUpRejectedDomainEvent : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; }
        public Resource Resource { get; }
        public string Reason { get; }
        public string Code { get; }

        public SignedUpRejectedDomainEvent(Guid requestId, Guid userId, string message, 
            Resource resource, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Resource = resource;
            Reason = reason;
            Code = code;
        }
    }
}
