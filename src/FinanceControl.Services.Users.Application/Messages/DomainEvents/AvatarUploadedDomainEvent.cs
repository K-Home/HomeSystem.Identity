using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class AvatarUploadedDomainEvent : IDomainEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public string AvatarUrl { get; }

        [JsonConstructor]
        public AvatarUploadedDomainEvent(Guid requestId, Guid userId, string avatarUrl)
        {
            RequestId = requestId;
            UserId = userId;
            AvatarUrl = avatarUrl;
        }
    }
}