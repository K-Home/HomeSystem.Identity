using HomeSystem.Services.Identity.Infrastructure.Files;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class UploadAvatar : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public File Avatar { get; }

        [JsonConstructor]
        public UploadAvatar(Request request, Guid userId, File avatar)
        {
            Request = request;
            UserId = userId;
            Avatar = avatar;
        }
    }
}
