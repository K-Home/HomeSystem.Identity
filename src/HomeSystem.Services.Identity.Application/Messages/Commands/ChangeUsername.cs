using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class ChangeUsername : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public string Name { get; }

        [JsonConstructor]
        public ChangeUsername(Request request, Guid userId, string name)
        {
            Request = request;
            UserId = userId;
            Name = name;
        }
    }
}
