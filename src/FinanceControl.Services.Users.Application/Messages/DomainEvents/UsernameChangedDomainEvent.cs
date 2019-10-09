using System;
using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class UsernameChangedDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; set; }
        public string Name { get; }
        public UsernameChangedDomainEvent(Guid requestId, 
            Guid userId, string name)
        {
            RequestId = requestId;
            UserId = userId;
            Name = name;
        }
    }
}