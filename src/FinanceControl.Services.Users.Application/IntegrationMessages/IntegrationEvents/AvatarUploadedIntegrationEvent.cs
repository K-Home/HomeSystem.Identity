using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class AvatarUploadedIntegrationEvent : IIntegrationEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; set; }
        public string AvatarUrl { get; }

        [JsonConstructor]
        public AvatarUploadedIntegrationEvent(Guid requestId, 
            Guid userId, string message, string avatarUrl)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            AvatarUrl = avatarUrl;
        }
    }
}
