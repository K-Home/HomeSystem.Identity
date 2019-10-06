using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignInRejectedDomainEvent : IDomainRejectedEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public string Message { get; }

        [DataMember]
        public string Code { get; }

        [DataMember]
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