using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class SignInCommand : ISessionCommand
    {
        public Request Request { get; }
        public Guid SessionId { get; }
        public string Email { get; }
        public string Password { get; }
        public string IpAddress { get; }
        public string UserAgent { get; }

        [JsonConstructor]
        public SignInCommand(Guid sessionId, string email,
            string password, string ipAddress, string userAgent)
        {
            Request = Request.New<SignInCommand>();
            SessionId = sessionId;
            Email = email;
            Password = password;
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }
    }
}