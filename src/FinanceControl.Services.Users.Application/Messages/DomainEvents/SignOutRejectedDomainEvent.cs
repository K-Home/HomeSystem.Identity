﻿using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignOutRejectedDomainEvent : IAuthenticationDomainEvent
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public string Message { get; }
        public string Reason { get; }
        public string Code { get; }

        public SignOutRejectedDomainEvent(Guid requestId, Guid userId, 
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