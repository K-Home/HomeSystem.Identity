using System;
using System.Runtime.Serialization;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class SignIn : ICommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid SessionId { get; }
        
        [DataMember]
        public string Email { get; }
        
        [DataMember]
        public string Password { get; }
        
        [DataMember]
        public string IpAddress { get; }
        
        [DataMember]
        public string AccessToken { get; }
        
        [JsonConstructor]
        public SignIn(Request request, Guid sessionId, string email, 
            string password, string ipAddress, string accessToken)
        {
            Request = request;
            SessionId = sessionId;
            Email = email;
            Password = password;
            IpAddress = ipAddress;
            AccessToken = accessToken;
        }
    }
}