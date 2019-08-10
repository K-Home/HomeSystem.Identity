using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
namespace HomeSystem.IntegrationMessages.IntegrationCommands
{
    public class RequestCreatedIntegrationCommand : IIntegrationCommand
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public Resource Resource { get; }

        [DataMember]
        public string Message { get; }

        [JsonConstructor]
        public RequestCreatedIntegrationCommand(Guid requestId, Guid userId, Resource resource, string message)
        {
            RequestId = requestId;
            UserId = userId;
            Resource = resource;
            Message = message;
        }
    }
}
