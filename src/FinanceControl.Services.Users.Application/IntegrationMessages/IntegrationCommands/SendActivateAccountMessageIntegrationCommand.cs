using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class SendActivateAccountMessageIntegrationCommand : IIntegrationCommand
    {
        [DataMember]
        public Request Request { get; }

        [DataMember]
        public string Username { get; }

        [DataMember]
        public string Email { get; }

        [DataMember]
        public string Token { get; }

        [DataMember]
        public string Endpoint { get; }

        [JsonConstructor]
        public SendActivateAccountMessageIntegrationCommand(Request request,
            string username, string email, string token, string endpoint)
        {
            Request = request;
            Username = username;
            Email = email;
            Token = token;
            Endpoint = endpoint;
        }
    }
}