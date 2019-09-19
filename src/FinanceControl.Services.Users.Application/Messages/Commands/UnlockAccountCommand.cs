using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class UnlockAccountCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public string UnlockUserId { get; }

        [JsonConstructor]
        public UnlockAccountCommand(Guid userId, string unlockUserId)
        {
            Request = Request.New<UnlockAccountCommand>();
            UserId = userId;
            UnlockUserId = unlockUserId;
        }
    }
}