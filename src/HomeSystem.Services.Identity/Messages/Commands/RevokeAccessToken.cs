using System;
using KShared.CQRS.Messages;
using Newtonsoft.Json;

namespace HomeSystem.Services.Identity.Messages.Commands
{
    public class RevokeAccessToken : ICommand
    {
        public Guid UserId { get; }
        public string Token { get; }

        public RevokeAccessToken(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}