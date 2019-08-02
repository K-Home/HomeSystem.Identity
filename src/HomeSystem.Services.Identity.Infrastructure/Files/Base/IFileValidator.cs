namespace HomeSystem.Services.Identity.Infrastructure.Files.Base
{
    public interface IFileValidator
    {
        bool IsImage(File file);
    }
}