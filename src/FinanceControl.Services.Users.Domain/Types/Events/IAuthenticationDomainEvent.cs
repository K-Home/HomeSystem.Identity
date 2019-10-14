using System;

namespace FinanceControl.Services.Users.Domain.Types.Events
{
    public interface IAuthenticationDomainEvent : IDomainEvent
    {
        Guid UserId { get; }
    }
}