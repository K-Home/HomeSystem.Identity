using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class SignedInIntegrationEvent : IIntegrationEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; }
        public string Email { get; }
        public string Name { get; }

        [JsonConstructor]
        public SignedInIntegrationEvent(Guid requestId, Guid userId,
            string message, string email, string name)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Email = email;
            Name = name;
        }
    }
}