using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
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
