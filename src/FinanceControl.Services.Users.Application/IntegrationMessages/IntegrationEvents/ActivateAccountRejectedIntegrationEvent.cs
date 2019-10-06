using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class ActivateAccountRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        public Guid RequestId { get; }
        public string Reason { get; }
        public string Code { get; }
        public string Email { get; }

        [JsonConstructor]
        public ActivateAccountRejectedIntegrationEvent(Guid requestId,
            string email, string code, string reason)
        {
            RequestId = requestId;
            Email = email;
            Code = code;
            Reason = reason;
        }
    }
}