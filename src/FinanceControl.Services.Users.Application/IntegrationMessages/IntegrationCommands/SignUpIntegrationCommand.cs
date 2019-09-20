using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class SignUpIntegrationCommand : IIntegrationCommand
    {
        [DataMember]
        public Request Request { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public Resource Resource { get; }

        [DataMember]
        public string Message { get; }

        [JsonConstructor]
        public SignUpIntegrationCommand(Request request, Guid userId, Resource resource, string message)
        {
            Request = request;
            UserId = userId;
            Resource = resource;
            Message = message;
        }
    }
}