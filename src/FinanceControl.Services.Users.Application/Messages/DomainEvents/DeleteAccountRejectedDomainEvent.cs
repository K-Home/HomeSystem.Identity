using System;
using FinanceControl.Services.Users.Domain.Types.Events;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class DeleteAccountRejectedDomainEvent : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public bool Soft { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public DeleteAccountRejectedDomainEvent(Guid requestId,
            Guid userId, bool soft, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            Soft = soft;
            Reason = reason;
            Code = code;
        }
    }
}