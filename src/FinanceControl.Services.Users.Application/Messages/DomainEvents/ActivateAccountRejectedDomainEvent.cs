using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class ActivateAccountRejectedDomainEvent : IDomainRejectedEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public string Reason { get; }

        [DataMember]
        public string Code { get; }

        [DataMember]
        public string Email { get; }

        [DataMember]
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