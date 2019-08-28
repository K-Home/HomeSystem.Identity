using System;
using System.Threading.Tasks;

namespace XSecure.Services.Users.Infrastructure.Files.Base
{
    public interface IFileHandler
    {
        Task UploadAsync(File file, string newName, Action<string, string> onUploaded = null);
        Task DeleteAsync(string name);
    }
}