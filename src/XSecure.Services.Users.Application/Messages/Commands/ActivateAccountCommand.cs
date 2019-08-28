using Newtonsoft.Json;
using System.Runtime.Serialization;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.Commands
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
