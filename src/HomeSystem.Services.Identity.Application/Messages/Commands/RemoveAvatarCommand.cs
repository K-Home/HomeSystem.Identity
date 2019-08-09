using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class RemoveAvatarCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }

        [JsonConstructor]
        public RemoveAvatarCommand(Request request, Guid userId)
        {
            Request = request;
            UserId = userId;
        }
    }
}
