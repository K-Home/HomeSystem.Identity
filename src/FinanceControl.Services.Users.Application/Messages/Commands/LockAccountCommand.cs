using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class LockAccountCommand : IAuthenticatedCommand
    {
        public Request Request { get; }
        public Guid UserId { get; }
        public string LockUserId { get; }

        [JsonConstructor]
        public LockAccountCommand(Guid userId, string lockUserId)
        {
            Request = Request.New<LockAccountCommand>();
            UserId = userId;
            LockUserId = lockUserId;
        }
    }
}