using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class RemoveAvatarCommand : IAuthenticatedCommand
    {
        public Request Request { get; }
        public Guid UserId { get; }

        [JsonConstructor]
        public RemoveAvatarCommand(Guid userId)
        {
            Request = Request.New<RemoveAvatarCommand>();
            UserId = userId;
        }
    }
}