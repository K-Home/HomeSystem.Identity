using System;
using FinanceControl.Services.Users.Domain.Types.Events;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class CreateActivateAccountSecuredOperationRejectedDomainEvent : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public Guid OperationId { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public CreateActivateAccountSecuredOperationRejectedDomainEvent(Guid requestId,
            Guid userId, Guid operationId, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            OperationId = operationId;
            Reason = reason;
            Code = code;
        }
    }
}