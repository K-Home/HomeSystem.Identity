using System;
using System.Runtime.Serialization;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticationRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        Guid UserId { get; set; }
    }
}