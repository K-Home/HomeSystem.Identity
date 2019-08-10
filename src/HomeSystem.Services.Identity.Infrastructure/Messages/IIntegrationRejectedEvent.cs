namespace HomeSystem.Services.Identity.Infrastructure.Messages
{
    public interface IIntegrationRejectedEvent : IIntegrationEvent
    {
        string Code { get; }
        string Reason { get; }
    }
}
