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
        public DeleteAccountCommand(Request request, Guid userId, bool soft)
        {
            Request = request;
            UserId = userId;
            Soft = soft;
        }
    }
}
