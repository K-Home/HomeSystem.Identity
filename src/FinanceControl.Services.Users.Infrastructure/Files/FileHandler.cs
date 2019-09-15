using System;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Infrastructure.Files.Base;

namespace FinanceControl.Services.Users.Infrastructure.Files
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
