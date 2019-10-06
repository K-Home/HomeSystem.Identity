namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IDomainRejectedEvent : IDomainEvent
    {
        string Reason { get; }
        
        string Code { get; }
    }
}