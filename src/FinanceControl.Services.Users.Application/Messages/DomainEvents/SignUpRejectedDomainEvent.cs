using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignUpRejectedDomainEvent : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public SignUpRejectedDomainEvent(Guid requestId, Guid userId,
            string message, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Reason = reason;
            Code = code;
        }
    }
}