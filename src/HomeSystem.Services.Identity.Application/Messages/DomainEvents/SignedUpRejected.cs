using HomeSystem.Services.Identity.Infrastructure.Messages;
using System;

namespace HomeSystem.Services.Identity.Application.Messages.DomainEvents
{
    public class SignedUpRejected : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; }
        public Resource Resource { get; }
        public string Reason { get; }
        public string Code { get; }

        public SignedUpRejected(Guid requestId, Guid userId, string message, 
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
