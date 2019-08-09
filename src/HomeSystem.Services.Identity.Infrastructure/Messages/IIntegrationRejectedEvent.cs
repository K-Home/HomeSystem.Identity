namespace HomeSystem.Services.Identity.Infrastructure.Messages
{
    public interface IIntegrationRejectedEvent
    {
        string Code { get; }
        string Reason { get; }
    }
}
