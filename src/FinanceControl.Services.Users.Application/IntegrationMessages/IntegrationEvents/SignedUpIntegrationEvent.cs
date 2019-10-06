using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class SignedUpIntegrationEvent : IIntegrationEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; }
        public string Role { get; }
        public string State { get; }

        [JsonConstructor]
        public SignedUpIntegrationEvent(Guid requestId, Guid userId,
            string message, string role, string state)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Role = role;
            State = state;
        }
    }
}