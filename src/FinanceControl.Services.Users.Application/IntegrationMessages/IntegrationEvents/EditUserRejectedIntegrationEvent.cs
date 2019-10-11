using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.IntegrationMessages
{
    public class EditUserRejectedIntegrationEvent : IIntegrationRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public EditUserRejectedIntegrationEvent(Guid requestId, Guid userId, 
            string message, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Reason = reason;
            Code = code;
        }
    }
}
