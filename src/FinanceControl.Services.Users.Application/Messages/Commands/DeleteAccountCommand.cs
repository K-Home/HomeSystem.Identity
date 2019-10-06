using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class DeleteAccountCommand : IAuthenticatedCommand
    {
        public Request Request { get; }
        public Guid UserId { get; }
        public bool Soft { get; }

        [JsonConstructor]
        public DeleteAccountCommand(Guid userId, bool soft)
        {
            Request = Request.New<DeleteAccountCommand>();
            UserId = userId;
            Soft = soft;
        }
    }
}