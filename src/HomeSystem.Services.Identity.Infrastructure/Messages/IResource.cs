namespace HomeSystem.Services.Identity.Infrastructure.Messages
{
    public interface IResource
    {
        string Service { get; }
        string EndPoint { get; }
    }
}
