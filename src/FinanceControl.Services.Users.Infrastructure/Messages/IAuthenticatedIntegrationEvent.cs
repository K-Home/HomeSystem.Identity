using System;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticatedIntegrationEvent : IIntegrationEvent
    {
        Guid UserId { get; }
    }
}