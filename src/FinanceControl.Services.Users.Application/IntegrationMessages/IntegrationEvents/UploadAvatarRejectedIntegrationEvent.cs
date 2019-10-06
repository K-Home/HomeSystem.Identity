using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class UploadAvatarRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Code { get; }

        [DataMember]
        public string Reason { get; }

        [JsonConstructor]
        public UploadAvatarRejectedIntegrationEvent(Guid requestId,
            Guid userId, string message, string code, string reason)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Code = code;
            Reason = reason;
        }
    }
}
