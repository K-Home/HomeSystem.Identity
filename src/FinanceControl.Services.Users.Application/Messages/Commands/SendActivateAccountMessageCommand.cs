using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class SendActivateAccountMessageCommand : ICommand
    {
        public Request Request { get; }
        public string Email { get; }
        public string Username { get; }
        public Guid UserId { get; }

        [JsonConstructor]
        public SendActivateAccountMessageCommand(Request request, string email,
            string username, Guid userId)
        {
            Request = request;
            Email = email;
            Username = username;
            UserId = userId;
        }
    }
}