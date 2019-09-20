using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class AccountActivatedDomainEvent : IDomainEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public string Email { get; }

        [DataMember]
        public string UserId { get; }

        [JsonConstructor]
        public AccountActivatedDomainEvent(Guid requestId, string email, string userId)
        {
            RequestId = requestId;
            Email = email;
            UserId = userId;
        }
    }
}
