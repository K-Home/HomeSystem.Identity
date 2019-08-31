using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
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
