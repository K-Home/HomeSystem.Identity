using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class ActivateAccountCommand : ICommand
    {
        public Request Request { get; }
        public string Email { get; }
        public string Token { get; }

        [JsonConstructor]
        public ActivateAccountCommand(string email, string token)
        {
            Request = Request.New<ActivateAccountCommand>();
            Email = email;
            Token = token;
        }
    }
}