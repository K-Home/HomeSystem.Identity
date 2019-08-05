using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class ChangePassword : IAuthenticatedCommand
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
        public ChangePassword(Request request, Guid userId, string currentPassword, string newPassword)
        {
            Request = request;
            UserId = userId;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
    }
}
