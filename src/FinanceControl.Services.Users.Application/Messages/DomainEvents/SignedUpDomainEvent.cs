using System.Runtime.Serialization;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class SignedUpDomainEvent : IDomainEvent
    {
        [DataMember]
        public Request Request { get; }

        [DataMember]
        public User User { get; }

        [DataMember]
        public string Message { get; }

        [JsonConstructor]
        public SignedUpDomainEvent(Request request, User user, string message)
        {
            Request = request;
            User = user;
            Message = message;
        }
    }
}