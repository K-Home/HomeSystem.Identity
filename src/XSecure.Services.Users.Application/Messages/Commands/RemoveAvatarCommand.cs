using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.Commands
{
    public class RemoveAvatarCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }

        [JsonConstructor]
        public RemoveAvatarCommand( Guid userId)
        {
            Request = Request.New<SignUpCommand>();
            UserId = userId;
        }
    }
}
