using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class AccountActivatedIntegrationEvent : IIntegrationEvent
    {
        public Guid RequestId { get; }
        public string Email { get; }
        public Guid UserId { get; }
        public string Message { get; }

        [JsonConstructor]
        public AccountActivatedIntegrationEvent(Guid requestId,
            string email, Guid userId, string message)
        {
            RequestId = requestId;
            Email = email;
            UserId = userId;
            Message = message;
        }
    }
}