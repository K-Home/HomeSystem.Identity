using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignedOutDomainEvent : IAuthenticationDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; }
        
        public SignedOutDomainEvent(Guid requestId, Guid userId, string message)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
        }
    }
}