using System;
using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SetNewPasswordRejectedDomainEvent : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public string Reason { get; }
        public string Code { get; }
        public string Email { get; }

        public SetNewPasswordRejectedDomainEvent(Guid requestId, 
            string code, string reason, string email)
        {
            RequestId = requestId;
            Reason = reason;
            Code = code;
            Email = email;
        }
    }
}
