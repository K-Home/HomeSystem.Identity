using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
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

        [DataMember]
        public string FirstName { get; }

        [DataMember]
        public string LastName { get; }

        [DataMember]
        public string Role { get; }
        
        [DataMember]
        public string State { get; }

        [JsonConstructor]
        public SignUpCommand(Request request, string email, string password, string userName, 
            string firstName, string lastName, string role, string state)
        {
            Request = request;
            Email = email;
            Password = password;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            State = state;
        }
    }
}