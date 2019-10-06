using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class CreateActivateAccountSecuredOperationRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public Guid OperationId { get; }
        public string Message { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public CreateActivateAccountSecuredOperationRejectedIntegrationEvent(Guid requestId, Guid userId,
            Guid operationId, string message, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            OperationId = operationId;
            Message = message;
            Reason = reason;
            Code = code;
        }
    }
}