using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class SetNewPasswordCommand : ICommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public string Email { get; }
        
        [DataMember]
        public string Token { get; }
        
        [DataMember]
        public string Password { get; }

        [JsonConstructor]
        public SetNewPasswordCommand(string email, string token, string password)
        {
            Request = Request.New<SignUpCommand>();
            Email = email;
            Token = token;
            Password = password;
        }
    }
}
