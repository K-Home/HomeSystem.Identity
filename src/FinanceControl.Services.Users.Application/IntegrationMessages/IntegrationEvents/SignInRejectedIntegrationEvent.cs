using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class SignInRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public string UserId { get; }

        [DataMember]
        public string Code { get; }

        [DataMember]
        public string Reason { get; }

        [JsonConstructor]
        public SignInRejectedIntegrationEvent(Guid requestId, string userId,
            string code, string reason)
        {
            RequestId = requestId;
            UserId = userId;
            Code = code;
            Reason = reason;
        }
    }
}