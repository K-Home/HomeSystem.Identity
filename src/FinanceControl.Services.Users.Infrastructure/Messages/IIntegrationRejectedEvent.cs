namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IIntegrationRejectedEvent : IIntegrationEvent
    {
        string Code { get; }
        string Reason { get; }
    }
}
