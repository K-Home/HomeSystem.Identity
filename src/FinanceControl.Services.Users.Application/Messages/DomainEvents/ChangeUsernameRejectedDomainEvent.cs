using System;
using FinanceControl.Services.Users.Domain.Types.Events;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class ChangeUsernameRejectedDomainEvent : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Name { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public ChangeUsernameRejectedDomainEvent(Guid requestId, Guid userId,
            string name, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            Name = name;
            Reason = reason;
            Code = code;
        }
    }
}