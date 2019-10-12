using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Messages.DomainEvents
{
    public class EnableTwoFactorAuthenticationRejectedDomainEvent : IDomainRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }
    }
}