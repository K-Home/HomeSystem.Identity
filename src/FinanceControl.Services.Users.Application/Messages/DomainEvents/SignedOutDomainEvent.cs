using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignedOutDomainEvent : IAuthenticationDomainEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public string Message { get; }

        public SignedOutDomainEvent(Guid requestId, Guid userId, string message)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
        }
    }
}