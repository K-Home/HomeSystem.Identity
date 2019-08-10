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
        public ChangeUsernameCommand(Guid userId, string name)
        {
            Request = Request.New<SignUpCommand>();
            UserId = userId;
            Name = name;
        }
    }
}
