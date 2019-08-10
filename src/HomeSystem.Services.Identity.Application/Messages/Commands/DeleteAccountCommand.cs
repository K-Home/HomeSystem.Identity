using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class DeleteAccountCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public bool Soft { get; }

        [JsonConstructor]
        public DeleteAccountCommand(Guid userId, bool soft)
        {
            Request = Request.New<SignUpCommand>();
            UserId = userId;
            Soft = soft;
        }
    }
}
