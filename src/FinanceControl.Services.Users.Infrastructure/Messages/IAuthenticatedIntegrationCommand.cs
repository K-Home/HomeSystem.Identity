using System;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticatedIntegrationCommand : IIntegrationCommand
    {
        Guid UserId { get; }
    }
}