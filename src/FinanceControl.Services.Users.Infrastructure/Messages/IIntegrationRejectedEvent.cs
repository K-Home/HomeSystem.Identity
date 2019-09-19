using System.Runtime.Serialization;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IIntegrationRejectedEvent : IIntegrationEvent
    {
        [DataMember]
        string Code { get; }

        [DataMember]
        string Reason { get; }
    }
}