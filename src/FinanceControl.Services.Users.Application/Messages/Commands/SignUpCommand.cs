using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class SignUpCommand : ICommand
    {
        public Request Request { get; }

        public string Email { get; }

        public string Password { get; }

        public string UserName { get; }

        [JsonConstructor]
        public SignUpCommand(string email, string password, string userName, string culture)
        {
            Request = Request.New<SignUpCommand>();
            Email = email;
            Password = password;
            UserName = userName;
        }
    }
}