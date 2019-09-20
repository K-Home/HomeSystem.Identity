﻿using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignedUpRejectedDomainEvent : IDomainRejectedEvent
    {
        [DataMember]
        public Guid RequestId { get; }
        
        [DataMember]
        public Guid UserId { get; }
        
        [DataMember]
        public string Message { get; }
        
        [DataMember]
        public Resource Resource { get; }
        
        [DataMember]
        public string Reason { get; }
        
        [DataMember]
        public string Code { get; }

        [JsonConstructor]
        public SignedUpRejectedDomainEvent(Guid requestId, Guid userId, string message,
            Resource resource, string reason, string code)
        {
            RequestId = requestId;
            UserId = userId;
            Message = message;
            Resource = resource;
            Reason = reason;
            Code = code;
        }
    }
}