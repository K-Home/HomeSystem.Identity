using System.Runtime.Serialization;
using MediatR;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IDomainRejectedEvent : INotification
    {
        [DataMember]
        string Reason { get; }

        [DataMember]
        string Code { get; }
    }
}