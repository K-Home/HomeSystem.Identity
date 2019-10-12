using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.IntegrationMessages
{
    // namespace must be the same in services, required by MassTransit library
    // https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
    // ReSharper disable once CheckNamespace
    public class SendResetPasswordMessageIntegrationCommand : IIntegrationCommand
    {
        public Request Request { get; }
        public string Email { get; }
        public string Token { get; }
        public string Endpoint { get; }

        [JsonConstructor]
        public SendResetPasswordMessageIntegrationCommand(Request request,
            string email, string token, string endpoint)
        {
            Request = request;
            Email = email;
            Token = token;
            Endpoint = endpoint;
        }
    }
}
