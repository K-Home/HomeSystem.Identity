using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Files;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class UploadAvatarCommand : IAuthenticatedCommand
    {
        [DataMember] public Request Request { get; }

        [DataMember] public Guid UserId { get; }

        [DataMember] public File Avatar { get; }

        [JsonConstructor]
        public UploadAvatarCommand(Guid userId, File avatar)
        {
            Request = Request.New<SignUpCommand>();
            UserId = userId;
            Avatar = avatar;
        }
    }
}