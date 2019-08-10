using HomeSystem.Services.Identity.Infrastructure.Files;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class UploadAvatarCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public File Avatar { get; }

        [JsonConstructor]
        public UploadAvatarCommand(Guid userId, File avatar)
        {
            Request = Request.New<SignUpCommand>();
            UserId = userId;
            Avatar = avatar;
        }
    }
}
