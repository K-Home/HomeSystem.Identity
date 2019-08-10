using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.IntegrationMessages.IntegrationCommands
{
    public class RequestCreatedIntegrationCommand : IIntegrationCommand
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public string Resource { get; }

        [DataMember]
        public string Message { get; }

        [JsonConstructor]
        public RequestCreatedIntegrationCommand(Guid requestId, Guid userId, string resource, string message)
        {
            RequestId = requestId;
            UserId = userId;
            Resource = resource;
            Message = message;
        }
    }
}
