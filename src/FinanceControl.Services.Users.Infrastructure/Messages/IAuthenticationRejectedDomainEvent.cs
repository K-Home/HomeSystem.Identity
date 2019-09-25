using System;
using System.Runtime.Serialization;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticationRejectedDomainEvent : IIntegrationRejectedEvent
    {
        Guid UserId { get; }
    }
}