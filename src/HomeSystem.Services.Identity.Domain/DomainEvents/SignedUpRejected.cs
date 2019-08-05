using HomeSystem.Services.Domain.DomainEvents.Base;
using System;

namespace HomeSystem.Services.Identity.Domain.DomainEvents
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
