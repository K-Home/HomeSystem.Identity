using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Domain.Types.Events;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Domain.DomainEvents
{
    public class SignedUpDomainEvent : IDomainEvent
    {
        public User User { get; }

        [JsonConstructor]
        public SignedUpDomainEvent(User user)
        {
            User = user;
        }
    }
}