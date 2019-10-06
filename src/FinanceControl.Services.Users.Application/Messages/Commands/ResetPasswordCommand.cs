using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class ResetPasswordCommand : ICommand
    {
        public Request Request { get; }
        public string Email { get; }
        public string Endpoint { get; }

        [JsonConstructor]
        public ResetPasswordCommand(string email, string endpoint)
        {
            Request = Request.New<ResetPasswordCommand>();
            Email = email;
            Endpoint = endpoint;
        }
    }
}