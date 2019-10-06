using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class SignOutCommand : IAuthenticatedCommand
    {
        public Request Request { get; }
        public Guid SessionId { get; }
        public Guid UserId { get; }

        [JsonConstructor]
        public SignOutCommand(Guid sessionId, Guid userId)
        {
            Request = Request.New<SignOutCommand>();
            SessionId = sessionId;
            UserId = userId;
        }
    }
}