using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class ChangePasswordRejectedDomainEvent : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public ChangePasswordRejectedDomainEvent(Guid requestId,
            Guid userId, string code, string reason)
        {
            RequestId = requestId;
            UserId = userId;
            Code = code;
            Reason = reason;
        }
    }
}