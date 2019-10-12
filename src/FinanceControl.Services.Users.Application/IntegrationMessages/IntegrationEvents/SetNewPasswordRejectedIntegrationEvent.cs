using System;
using FinanceControl.Services.Users.Infrastructure.Messages;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class SetNewPasswordRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        public Guid RequestId { get; }
        public string Reason { get; }
        public string Code { get; }
        public string Email { get; }
        public string Message { get; }

        public SetNewPasswordRejectedIntegrationEvent(Guid requestId, 
            string code, string reason, string email, string message)
        {
            RequestId = requestId;
            Reason = reason;
            Code = code;
            Email = email;
            Message = message;
        }
    }
}