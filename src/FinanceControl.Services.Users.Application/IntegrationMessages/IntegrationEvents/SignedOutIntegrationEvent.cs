using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class SignedOutIntegrationEvent : IAuthenticatedIntegrationEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public string Message { get; }

        public SignedOutIntegrationEvent(Guid requestId, Guid userId, string message)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
        }
    }
}