using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class SendActivateAccountMessageIntegrationCommand : IIntegrationCommand
    {
        public Request Request { get; }
        public string Email { get; }
        public string Username { get; }
        public string Token { get; }
        public string Endpoint { get; }

        [JsonConstructor]
        public SendActivateAccountMessageIntegrationCommand(Request request,
            string email, string username, string token, string endpoint)
        {
            Request = request;
            Email = email;
            Username = username;
            Token = token;
            Endpoint = endpoint;
        }
    }
}