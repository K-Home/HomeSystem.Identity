﻿using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignedOutDomainEvent : IAuthenticationDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }

        [JsonConstructor]
        public SignedOutDomainEvent(Guid requestId, Guid userId)
        {
            RequestId = requestId;
            UserId = userId;
        }
    }
}