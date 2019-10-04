namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IFileUploadIntegrationCommand : IAuthenticatedIntegrationCommand
    {
        string Name { get; }
        string ContentType { get; }
        string FileBase64 { get; }
    }
}