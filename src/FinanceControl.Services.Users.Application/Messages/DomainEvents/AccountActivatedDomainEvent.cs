using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class AccountActivatedDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public string Email { get; }
        public Guid UserId { get; }

        [JsonConstructor]
        public AccountActivatedDomainEvent(Guid requestId, string email, Guid userId)
        {
            RequestId = requestId;
            Email = email;
            UserId = userId;
        }
    }
}