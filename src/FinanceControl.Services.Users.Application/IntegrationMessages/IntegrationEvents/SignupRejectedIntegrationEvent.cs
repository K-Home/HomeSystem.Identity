using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class SignUpRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; }
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public SignUpRejectedIntegrationEvent(Guid requestId, Guid userId,
            string message, string code, string reason)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Code = code;
            Reason = reason;
        }
    }
}