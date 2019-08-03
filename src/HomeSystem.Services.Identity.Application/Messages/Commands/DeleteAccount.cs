using System;
using System.Runtime.Serialization;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class DeleteAccount : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public bool Soft { get; }

        [JsonConstructor]
        public DeleteAccount(Request request, Guid userId, bool soft)
        {
            Request = request;
            UserId = userId;
            Soft = soft;
        }
    }
}
