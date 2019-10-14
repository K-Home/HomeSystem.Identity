using System;
using FinanceControl.Services.Users.Domain.Types.Events;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class NewPasswordSetDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public string Email { get; }

        public NewPasswordSetDomainEvent(Guid requestId, string email)
        {
            RequestId = requestId;
            Email = email;
        }
    }
}