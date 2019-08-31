using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class LockAccountCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public string LockUserId { get; }

        [JsonConstructor]
        public LockAccountCommand(Guid userId, string lockUserId)
        {
            Request = Request.New<SignUpCommand>();
            UserId = userId;
            LockUserId = lockUserId;
        }
    }
}
