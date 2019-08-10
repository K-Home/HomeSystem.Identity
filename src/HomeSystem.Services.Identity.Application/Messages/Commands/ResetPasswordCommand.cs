using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
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
