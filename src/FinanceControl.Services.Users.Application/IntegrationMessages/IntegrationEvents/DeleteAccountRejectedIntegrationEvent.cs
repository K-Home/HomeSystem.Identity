using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class DeleteAccountRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public bool Soft { get; }
        public string Message { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public DeleteAccountRejectedIntegrationEvent(Guid requestId, Guid userId,
            bool soft, string message, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            Soft = soft;
            Message = message;
            Reason = reason;
            Code = code;
        }
    }
}