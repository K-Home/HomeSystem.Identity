﻿using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class ResetPasswordRejectedDomainEvent : IDomainRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }
    }
}