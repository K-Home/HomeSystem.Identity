using System;
using FinanceControl.Services.Users.Domain.Types.Events;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class ResetPasswordRejectedDomainEvent : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public string Email { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public ResetPasswordRejectedDomainEvent(Guid requestId,
            string email, string reason, string code)
        {
            RequestId = requestId;
            Email = email;
            Reason = reason;
            Code = code;
        }
    }
}