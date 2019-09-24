using System.Runtime.Serialization;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class SendActivateAccountMessageWhenSignedUpCommand : ICommand
    {
        [DataMember]
        public Request Request { get; }

        [DataMember]
        public User User { get; }

        [JsonConstructor]
        public SendActivateAccountMessageWhenSignedUpCommand(Request request, User user)
        {
            Request = request;
            User = user;
        }
    }
}