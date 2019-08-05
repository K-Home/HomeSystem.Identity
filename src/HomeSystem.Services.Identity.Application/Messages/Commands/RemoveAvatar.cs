using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class RemoveAvatar : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }

        [JsonConstructor]
        public RemoveAvatar(Request request, Guid userId)
        {
            Request = request;
            UserId = userId;
        }
    }
}
