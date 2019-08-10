using System;
using HomeSystem.Services.Identity.Infrastructure.Messages;

namespace HomeSystem.Services.Identity.Application.Messages.DomainEvents
{
    public class SignedUpRejected : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Reason { get; }
        public string Code { get; }

        public SignedUpRejected(Guid requestId, Guid userId, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            Reason = reason;
            Code = code;
        }
    }
}
