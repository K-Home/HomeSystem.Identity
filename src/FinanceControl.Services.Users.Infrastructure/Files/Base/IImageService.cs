using System.Collections.Generic;

namespace FinanceControl.Services.Users.Infrastructure.Files.Base
{
    public interface IImageService
    {
        File ProcessImage(File file, double size);
        IDictionary<string, File> ProcessImage(File file);
    }
}