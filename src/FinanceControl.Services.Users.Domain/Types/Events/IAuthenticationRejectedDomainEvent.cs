using System;

namespace FinanceControl.Services.Users.Domain.Types.Events
{
    public interface IAuthenticationRejectedDomainEvent : IDomainRejectedEvent
    {
        Guid UserId { get; }
    }
}