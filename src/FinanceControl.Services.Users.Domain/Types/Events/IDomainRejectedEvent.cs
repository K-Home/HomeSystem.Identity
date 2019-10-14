namespace FinanceControl.Services.Users.Domain.Types.Events
{
    public interface IDomainRejectedEvent : IDomainEvent
    {
        string Reason { get; }

        string Code { get; }
    }
}