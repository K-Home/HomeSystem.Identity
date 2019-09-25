using System;
using System.Runtime.Serialization;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticatedIntegrationCommand : IIntegrationCommand
    {
        Guid UserId { get; }
    }
}