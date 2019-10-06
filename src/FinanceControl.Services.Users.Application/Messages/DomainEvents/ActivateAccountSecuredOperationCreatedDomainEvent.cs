using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class ActivateAccountSecuredOperationCreatedDomainEvent : IDomainEvent
    {
        public Request Request { get; }
        public Guid UserId { get; }
        public string Username { get; }
        public string Email { get; }
        public Guid OperationId { get; }
        public string Token { get; }
        public string Endpoint { get; }

        [JsonConstructor]
        public ActivateAccountSecuredOperationCreatedDomainEvent(Request request, Guid userId,
            string username, string email, Guid operationId, string token, string endpoint)
        {
            Request = request;
            UserId = userId;
            Username = username;
            Email = email;
            OperationId = operationId;
            Token = token;
            Endpoint = endpoint;
        }
    }
}