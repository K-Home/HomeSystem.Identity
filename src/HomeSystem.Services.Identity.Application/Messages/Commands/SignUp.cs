using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class SignUp : ICommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public string Email { get; }
        
        [DataMember]
        public string Password { get; }
        
        [DataMember]
        public string Name { get; }

        [DataMember]
        public string FirstName { get; }

        [DataMember]
        public string LastName { get; }

        [DataMember]
        public string Role { get; }
        
        [DataMember]
        public string State { get; }
        
        [DataMember]
        public string AccessToken { get; }

        [JsonConstructor]
        public SignUp(Request request, string email, string password, string name, 
            string firstName, string lastName, string role, string state, string accessToken)
        {
            Request = request;
            Email = email;
            Password = password;
            Name = name;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            State = state;
            AccessToken = accessToken;
        }
    }
}