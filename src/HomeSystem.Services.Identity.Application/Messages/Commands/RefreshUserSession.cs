using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class RefreshUserSession : ICommand
    {
        [DataMember]
        public Request Request { get; }
        
        [DataMember]
        public Guid SessionId { get; }
        
        [DataMember]
        public Guid NewSessionId { get; }
        
        [DataMember]
        public string Key { get; }

        [JsonConstructor]
        public RefreshUserSession(Request request, Guid sessionId, Guid newSessionId, string key)
        {
            Request = request;
            SessionId = sessionId;
            NewSessionId = newSessionId;
            Key = key;
        }
    }
}
