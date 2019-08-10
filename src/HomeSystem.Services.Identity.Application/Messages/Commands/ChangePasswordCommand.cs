using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class ChangePasswordCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public string CurrentPassword { get; }
        
        [DataMember]
        public string NewPassword { get; }

        [JsonConstructor]
        public ChangePasswordCommand(Guid userId, string currentPassword, string newPassword)
        {
            Request = Request.New<SignUpCommand>();
            UserId = userId;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
    }
}
