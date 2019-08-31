using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class DeleteAccountCommand : IAuthenticatedCommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public bool Soft { get; }

        [JsonConstructor]
        public DeleteAccountCommand(Guid userId, bool soft)
        {
            Request = Request.New<SignUpCommand>();
            UserId = userId;
            Soft = soft;
        }
    }
}
