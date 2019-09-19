using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class SignInCommand : ICommand
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
        public SignInCommand(Guid sessionId, string email,
            string password, string ipAddress, string accessToken)
        {
            Request = Request.New<SignInCommand>();
            SessionId = sessionId;
            Email = email;
            Password = password;
            IpAddress = ipAddress;
            AccessToken = accessToken;
        }
    }
}