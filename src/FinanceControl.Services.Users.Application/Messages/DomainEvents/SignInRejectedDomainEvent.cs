using System;
using FinanceControl.Services.Users.Domain.Types.Events;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignInRejectedDomainEvent : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public SignInRejectedDomainEvent(Guid requestId, Guid userId,
            string code, string reason)
        {
            RequestId = requestId;
            UserId = userId;
            Code = code;
            Reason = reason;
        }
    }
}