using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class SendActivateAccountMessageCommand : ICommand
    {
        [DataMember]
        public Request Request { get; }

        [DataMember]
        public string Email { get; }
        
        [DataMember]
        public string Username { get; }
        
        [DataMember]
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