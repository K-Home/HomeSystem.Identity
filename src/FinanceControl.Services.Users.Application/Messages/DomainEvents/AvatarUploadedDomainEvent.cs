using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class AvatarUploadedDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
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