﻿using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class AccountActivatedDomainEvent : IDomainEvent
    {
        public Guid RequestId { get; }
        public string Email { get; }
        public Guid UserId { get; }
        public string Message { get; }

        [JsonConstructor]
        public AccountActivatedDomainEvent(Guid requestId, string email, Guid userId, string message)
        {
            RequestId = requestId;
            Email = email;
            UserId = userId;
            Message = message;
        }
    }
}