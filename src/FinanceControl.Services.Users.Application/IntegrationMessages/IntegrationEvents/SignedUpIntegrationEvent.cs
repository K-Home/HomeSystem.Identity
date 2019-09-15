using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
namespace FinanceControl.Services.Users.Application.IntegrationMessages.IntegrationEvents
{
    public class SignedUpIntegrationEvent : IIntegrationEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public string Message { get; }

        [DataMember]
        public Resource Resource { get; }

        [DataMember]
        public string Role { get; }

        [DataMember]
        public string State { get; }

        [JsonConstructor]
        public SignedUpIntegrationEvent(Guid requestId, Guid userId,
            string message, Resource resource, string role, string state)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Resource = resource;
            Role = role;
            State = state;
        }
    }
}