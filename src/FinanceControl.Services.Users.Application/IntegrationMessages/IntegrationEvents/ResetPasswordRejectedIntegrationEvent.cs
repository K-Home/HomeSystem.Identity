using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class ResetPasswordRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        public Guid RequestId { get; }
        public string Email { get; }
        public string Message { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public ResetPasswordRejectedIntegrationEvent(Guid requestId,
            string email, string message, string reason, string code)
        {
            RequestId = requestId;
            Email = email;
            Message = message;
            Reason = reason;
            Code = code;
        }
    }
}