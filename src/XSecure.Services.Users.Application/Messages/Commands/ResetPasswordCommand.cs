using Newtonsoft.Json;
using System.Runtime.Serialization;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.Commands
{
    public class ResetPasswordCommand : ICommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public string Email { get; }
        
        [DataMember]
        public string Endpoint { get; }

        [JsonConstructor]
        public ResetPasswordCommand(string email, string endpoint)
        {
            Request = Request.New<SignUpCommand>();
            Email = email;
            Endpoint = endpoint;
        }
    }
}
