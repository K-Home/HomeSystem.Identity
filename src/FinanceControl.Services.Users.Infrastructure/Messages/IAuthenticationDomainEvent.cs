using System;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticationDomainEvent : IDomainEvent
    {
        Guid UserId { get; }
    }
}