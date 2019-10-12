﻿using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class DisableTwoFactorAuthenticationRejectedDomainEvent : IDomainRejectedEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public DisableTwoFactorAuthenticationRejectedDomainEvent(Guid requestId, 
            Guid userId, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            Reason = reason;
            Code = code;
        }
    }
}