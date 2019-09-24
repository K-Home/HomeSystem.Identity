using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class SendActivateAccountMessageCommand : ICommand
    {
        [DataMember]
        public Request Request { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public string Username { get; }

        [DataMember]
        public string Email { get; }

        [JsonConstructor]
        public SendActivateAccountMessageCommand(Request request,
            Guid userId, string username, string email)
        {
            Request = request;
            UserId = userId;
            Username = username;
            Email = email;
        }
    }
}