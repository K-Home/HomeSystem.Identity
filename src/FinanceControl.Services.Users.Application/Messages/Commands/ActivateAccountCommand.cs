using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class ActivateAccountCommand : ICommand
    {
        [DataMember]
        public Request Request { get; }

        [DataMember]
        public string Email { get; }

        [DataMember]
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