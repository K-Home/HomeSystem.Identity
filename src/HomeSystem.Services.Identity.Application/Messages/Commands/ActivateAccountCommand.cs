using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
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
            Request = Request.New<SignUpCommand>();
            Email = email;
            Token = token;
        }
    }
}
