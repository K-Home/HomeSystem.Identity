using HomeSystem.Services.Identity.Infrastructure.Files.Base;
using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Infrastructure.Files
{
    public class FileHandler : IFileHandler
    {
        public Task UploadAsync(File file, string newName, Action<string, string> onUploaded = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
