using System;
using System.Runtime.Serialization;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IIntegrationEvent
    {
        [DataMember]
        Guid RequestId { get; }
    }
}