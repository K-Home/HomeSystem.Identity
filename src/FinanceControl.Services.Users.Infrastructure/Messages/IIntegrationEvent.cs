using System;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IIntegrationEvent
    {
        Guid RequestId { get; }
    }
}