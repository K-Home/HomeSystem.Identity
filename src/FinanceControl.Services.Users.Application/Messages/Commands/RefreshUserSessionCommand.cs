using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class RefreshUserSessionCommand : ISessionCommand
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
            Request = Request.New<RefreshUserSessionCommand>();
            SessionId = sessionId;
            NewSessionId = newSessionId;
            Key = key;
        }
    }
}