using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class SignOutCommand : IAuthenticatedCommand
    {
        [DataMember] 
        public Request Request { get; }

        [DataMember] 
        public Guid SessionId { get; }

        [DataMember] 
        public Guid UserId { get; }

        [JsonConstructor]
        public SignOutCommand(Request request, Guid sessionId, Guid userId)
        {
            Request = request;
            SessionId = sessionId;
            UserId = userId;
        }
    }
}