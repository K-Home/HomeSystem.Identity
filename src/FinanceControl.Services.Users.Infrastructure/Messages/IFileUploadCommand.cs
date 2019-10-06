namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IFileUploadCommand : IAuthenticatedCommand
    {
        string Filename { get; }
        string FileContentType { get; }
        string FileBase64 { get; }
    }
}