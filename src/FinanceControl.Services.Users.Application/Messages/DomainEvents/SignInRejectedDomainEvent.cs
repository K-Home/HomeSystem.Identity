using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignInRejectedDomainEvent : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; }
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public SignInRejectedDomainEvent(Guid requestId, Guid userId,
            string message, string code, string reason)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Code = code;
            Reason = reason;
        }
    }
}