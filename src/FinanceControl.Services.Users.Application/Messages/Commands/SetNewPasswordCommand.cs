using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class SetNewPasswordCommand : ICommand
    {
        public Request Request { get; }
        public string Email { get; }
        public string Token { get; }
        public string Password { get; }

        [JsonConstructor]
        public SetNewPasswordCommand(string email, string token, string password)
        {
            Request = Request.New<SetNewPasswordCommand>();
            Email = email;
            Token = token;
            Password = password;
        }
    }
}