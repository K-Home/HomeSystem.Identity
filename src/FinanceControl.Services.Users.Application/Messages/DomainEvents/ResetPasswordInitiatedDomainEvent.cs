using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class ResetPasswordInitiatedDomainEvent : IDomainEvent
    {
        public Request Request { get; }
        public Guid OperationId { get; }
        public string Email { get; }
        public string Endpoint { get; }

        [JsonConstructor]
        public ResetPasswordInitiatedDomainEvent(Request request,
            Guid operationId, string email, string endpoint)
        {
            Request = request;
            OperationId = operationId;
            Email = email;
            Endpoint = endpoint;
        }
    }
}