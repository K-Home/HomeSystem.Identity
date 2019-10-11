using System;
using FinanceControl.Services.Users.Infrastructure.Messages;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class ChangeUsernameRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Name { get; }
        public string Message { get; }
        public string Reason { get; }
        public string Code { get; }

        public ChangeUsernameRejectedIntegrationEvent(Guid requestId, Guid userId,
            string name, string message, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            Name = name;
            Message = message;
            Reason = reason;
            Code = code;
        }
    }
}
