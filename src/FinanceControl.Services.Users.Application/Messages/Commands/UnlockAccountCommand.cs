using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class UnlockAccountCommand : IAuthenticatedCommand
    {
        public Request Request { get; }
        public Guid UserId { get; }
        public Guid UnlockUserId { get; }

        [JsonConstructor]
        public UnlockAccountCommand(Guid userId, Guid unlockUserId)
        {
            Request = Request.New<UnlockAccountCommand>();
            UserId = userId;
            UnlockUserId = unlockUserId;
        }
    }
}