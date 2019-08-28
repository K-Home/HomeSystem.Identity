using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using XSecure.Services.Users.Infrastructure.Messages;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
namespace XSecure.IntegrationMessages.IntegrationEvents
{
    public class SignUpRejectedIntegrationEvent : IIntegrationRejectedEvent
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
        public string Code { get; }

        [DataMember]
        public string Reason { get; }

        [JsonConstructor]
        public SignUpRejectedIntegrationEvent(Guid requestId, Guid userId,
            string message, Resource resource, string code, string reason)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Resource = resource;
            Code = code;
            Reason = reason;
        }
    }
}