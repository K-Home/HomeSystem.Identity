using System;
using System.Runtime.Serialization;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class SetNewPassword : ICommand
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
        public SetNewPassword(Request request, string email, string token, string password)
        {
            Request = request;
            Email = email;
            Token = token;
            Password = password;
        }
    }
}
