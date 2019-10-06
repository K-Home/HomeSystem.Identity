﻿using System.Runtime.Serialization;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignedUpDomainEvent : IDomainEvent
    {
        public Request Request { get; }
        public User User { get; }
        public string Message { get; }

        [JsonConstructor]
        public SignedUpDomainEvent(Request request, User user, string message)
        {
            Request = request;
            User = user;
            Message = message;
        }
    }
}