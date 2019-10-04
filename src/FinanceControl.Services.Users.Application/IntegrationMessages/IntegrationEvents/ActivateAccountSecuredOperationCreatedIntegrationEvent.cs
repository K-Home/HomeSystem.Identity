using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class ActivateAccountSecuredOperationCreatedIntegrationEvent : IIntegrationEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public Guid OperationId { get; }

        [DataMember]
        public string Message { get; }

        [JsonConstructor]
        public ActivateAccountSecuredOperationCreatedIntegrationEvent(Guid requestId, Guid userId,
            Guid operationId, string message)
        {
            RequestId = requestId;
            UserId = userId;
            OperationId = operationId;
            Message = message;
        }
    }
}