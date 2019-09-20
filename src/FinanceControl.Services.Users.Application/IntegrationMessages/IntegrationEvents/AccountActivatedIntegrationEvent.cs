using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class AccountActivatedIntegrationEvent : IIntegrationEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public string Email { get; }

        [DataMember]
        public string UserId { get; }

        [JsonConstructor]
        public AccountActivatedIntegrationEvent(Guid requestId, string email, string userId)
        {
            RequestId = requestId;
            Email = email;
            UserId = userId;
        }
    }
}