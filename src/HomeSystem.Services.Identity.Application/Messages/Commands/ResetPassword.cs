using System;
using System.Runtime.Serialization;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class ResetPassword : ICommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public string Email { get; }
        
        [DataMember]
        public string Endpoint { get; }

        [JsonConstructor]
        public ResetPassword(Request request, string email, string endpoint)
        {
            Request = request;
            Email = email;
            Endpoint = endpoint;
        }
    }
}
