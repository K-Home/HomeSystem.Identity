using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class ActivateAccountSecuredOperationCreatedDomainEvent : IDomainEvent
    {
        [DataMember]
        public Request Request { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public string Username { get; }

        [DataMember]
        public string Email { get; }

        [DataMember]
        public Guid OperationId { get; }

        [DataMember]
        public string Message { get; }

        [DataMember]
        public string Token { get; }

        [DataMember]
        public string Endpoint { get; }

        [JsonConstructor]
        public ActivateAccountSecuredOperationCreatedDomainEvent(Request request, Guid userId,
            string username, string email, Guid operationId, string message, string token, string endpoint)
        {
            Request = request;
            UserId = userId;
            Username = username;
            Email = email;
            OperationId = operationId;
            Message = message;
            Token = token;
            Endpoint = endpoint;
        }
    }
}