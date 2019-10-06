using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class RefreshUserSessionCommand : ISessionCommand
    {
        public Request Request { get; }
        public Guid SessionId { get; }
        public Guid NewSessionId { get; }
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