using System;
using FinanceControl.Services.Users.Infrastructure.Messages;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class NewPasswordSetIntegrationEvent : IIntegrationEvent
    {
        public Guid RequestId { get; }
        public string Email { get; }
        public string Message { get; }

        public NewPasswordSetIntegrationEvent(Guid requestId, string email, string message)
        {
            RequestId = requestId;
            Email = email;
            Message = message;
        }
    }
}