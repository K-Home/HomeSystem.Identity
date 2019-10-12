using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class DisableTwoFactorAuthenticationRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Reason { get; }
        public string Code { get; }
        public string Message { get; }

        [JsonConstructor]
        public DisableTwoFactorAuthenticationRejectedIntegrationEvent(Guid requestId,
            Guid userId, string reason, string code, string message)
        {
            RequestId = requestId;
            UserId = userId;
            Reason = reason;
            Code = code;
            Message = message;
        }
    }
}