using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class CreateActivateAccountSecuredOperationRejectedDomainEvent : IDomainRejectedEvent
    {
        [DataMember]
        public Guid RequestId { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public Guid OperationId { get; }

        [DataMember]
        public string Message { get; }

        [DataMember]
        public string Reason { get; }

        [DataMember]
        public string Code { get; }

        [JsonConstructor]
        public CreateActivateAccountSecuredOperationRejectedDomainEvent(Guid requestId, Guid userId,
            Guid operationId, string message, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            OperationId = operationId;
            Message = message;
            Reason = reason;
            Code = code;
        }
    }
}