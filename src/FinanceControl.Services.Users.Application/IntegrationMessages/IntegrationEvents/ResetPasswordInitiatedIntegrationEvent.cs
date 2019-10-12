using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class ResetPasswordInitiatedIntegrationEvent : IIntegrationEvent
    {
        public Guid RequestId { get; }
        public Guid OperationId { get; }
        public string Email { get; }
        public string Endpoint { get; }
        public string Message { get; }

        [JsonConstructor]
        public ResetPasswordInitiatedIntegrationEvent(Guid requestId,
            Guid operationId, string email, string endpoint, string message)
        {
            RequestId = requestId;
            OperationId = operationId;
            Email = email;
            Endpoint = endpoint;
            Message = message;
        }
    }
}