using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class ChangeUsernameCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public string Name { get; }

        [JsonConstructor]
        public ChangeUsernameCommand(Request request, Guid userId, string name)
        {
            Request = request;
            UserId = userId;
            Name = name;
        }
    }
}
