using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.IntegrationMessages.IntegrationEvents
{
    public class SignedUpIntegrationEvent : IIntegrationEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public Resource Resource { get; }

        [DataMember]
        public string Role { get; }

        [DataMember]
        public string State { get; }

        [JsonConstructor]
        public SignedUpIntegrationEvent(Guid requestId, Guid userId, 
            Resource resource, string role, string state)
        {
            RequestId = requestId;
            UserId = userId;
            Resource = resource;
            Role = role;
            State = state;
        }
    }
}
