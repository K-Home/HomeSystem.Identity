using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class RefreshUserSessionCommand : ICommand
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
        public RefreshUserSessionCommand(Guid sessionId, Guid newSessionId, string key)
        {
            Request = Request.New<SignUpCommand>();
            SessionId = sessionId;
            NewSessionId = newSessionId;
            Key = key;
        }
    }
}
