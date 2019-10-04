using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class SignUpCommand : ICommand
    {
        [DataMember]
        public Request Request { get; }

        [DataMember]
        public string Email { get; }

        [DataMember]
        public string Password { get; }

        [DataMember]
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