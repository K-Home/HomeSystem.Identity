using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class ActivateAccountRejectedDomainEvent : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public string Reason { get; }
        public string Code { get; }
        public string Email { get; }
        public string Message { get; }

        [JsonConstructor]
        public ActivateAccountRejectedDomainEvent(Guid requestId,
            string email, string code, string reason, string message)
        {
            RequestId = requestId;
            Email = email;
            Code = code;
            Reason = reason;
            Message = message;
        }
    }
}