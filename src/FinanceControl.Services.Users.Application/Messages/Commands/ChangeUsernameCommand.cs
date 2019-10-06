using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class ChangeUsernameCommand : IAuthenticatedCommand
    {
        public Request Request { get; }
        public Guid UserId { get; }
        public string Name { get; }

        [JsonConstructor]
        public ChangeUsernameCommand(Guid userId, string name)
        {
            Request = Request.New<ChangeUsernameCommand>();
            UserId = userId;
            Name = name;
        }
    }
}