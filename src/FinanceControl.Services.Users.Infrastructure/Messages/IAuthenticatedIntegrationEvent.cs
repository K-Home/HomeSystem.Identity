﻿using System;
using System.Runtime.Serialization;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IAuthenticatedIntegrationEvent : IIntegrationEvent
    {
        [DataMember]
        Guid UserId { get; }
    }
}