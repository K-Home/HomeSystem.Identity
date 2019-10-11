using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class LockAccountCommand : IAuthenticatedCommand
    {
        public Request Request { get; }
        public Guid UserId { get; }
        public Guid LockUserId { get; }

        [JsonConstructor]
        public LockAccountCommand(Guid userId, Guid lockUserId)
        {
            Request = Request.New<LockAccountCommand>();
            UserId = userId;
            LockUserId = lockUserId;
        }
    }
}