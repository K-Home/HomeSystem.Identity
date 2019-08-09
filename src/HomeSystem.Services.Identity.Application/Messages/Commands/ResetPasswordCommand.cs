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
        public ResetPasswordCommand(Request request, string email, string endpoint)
        {
            Request = request;
            Email = email;
            Endpoint = endpoint;
        }
    }
}
