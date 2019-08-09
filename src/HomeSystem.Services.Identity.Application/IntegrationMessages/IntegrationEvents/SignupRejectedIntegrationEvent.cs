using System;
using System.Runtime.Serialization;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;

namespace HomeSystem.Services.Identity.Application.IntegrationMessages.IntegrationEvents
{
    public class SignUpRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public string Code { get; }

        [DataMember]
        public string Reason { get; }

        [JsonConstructor]
        public SignUpRejectedIntegrationEvent(Guid requestId, Guid userId, string code, string reason)
        {
            RequestId = requestId;
            UserId = userId;
            Code = code;
            Reason = reason;
        }
    }
}
