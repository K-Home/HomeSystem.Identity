using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

// namespace must be the same in services, required by MassTransit library
// https://stackoverflow.com/questions/52477283/masstransit-consume-equal-objects-defined-in-different-namespaces
// ReSharper disable once CheckNamespace
namespace FinanceControl.IntegrationMessages
{
    public class LockAccountRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public Guid LockedUserId { get; }
        public string Message { get; }
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public LockAccountRejectedIntegrationEvent(Guid requestId, Guid userId,
            Guid lockedUserId, string message, string code, string reason)
        {
            RequestId = requestId;
            UserId = userId;
            LockedUserId = lockedUserId;
            Message = message;
            Code = code;
            Reason = reason;
        }
    }
}