using System;
using System.Runtime.Serialization;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticationDomainEvent : IDomainEvent
    {
        Guid UserId { get; }
    }
}