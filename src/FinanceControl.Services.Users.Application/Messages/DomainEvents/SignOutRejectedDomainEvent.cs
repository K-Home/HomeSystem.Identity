using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignOutRejectedDomainEvent : IAuthenticationDomainEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public string Message { get; }

        [DataMember]
        public string Reason { get; }

        [DataMember]
        public string Code { get; }

        public SignOutRejectedDomainEvent(Guid requestId, Guid userId, 
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