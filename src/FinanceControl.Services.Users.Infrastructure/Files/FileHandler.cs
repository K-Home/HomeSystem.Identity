using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Files.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Services.Users.Infrastructure.Files
{
    public class FileHandler : IFileHandler
    {
        private readonly ILogger<FileHandler> _logger;
        private readonly IHostingEnvironment _applicationEnvironment;

        public FileHandler(ILogger<FileHandler> logger, IHostingEnvironment applicationEnvironment)
        {
            _logger = logger.CheckIfNotEmpty();
            _applicationEnvironment = applicationEnvironment.CheckIfNotEmpty();
        }

        public async Task UploadAsync(File file, string newName, Action<string, string> onUploaded)
        {
            var baseUrl = Path.Combine(_applicationEnvironment.WebRootPath, "uploadedImages");
            var fullUrl = $"{baseUrl}/{newName}";

            _logger.LogInformation($"Uploading file {file.Name} -> {newName} to: {baseUrl}");
            CreateDirectoryWhenNotExist(baseUrl);

            using (var stream = new MemoryStream(file.Bytes))
            {
                var image = Image.FromStream(stream);
                image.Save(fullUrl);
            }

            _logger.LogInformation($"Completed uploading file {file.Name} -> {newName} to: {baseUrl}.");
            onUploaded?.Invoke(baseUrl, fullUrl);

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(string name)
        {
            var baseUrl = Path.Combine(_applicationEnvironment.WebRootPath, "uploadedImages");
            var fullUrl = $"{baseUrl}/{name}";

            _logger.LogInformation($"Deleting file {name} from: {baseUrl}.");

            if (System.IO.File.Exists(fullUrl))
            {
                System.IO.File.Delete(fullUrl);
            }

            _logger.LogInformation($"Completed deleting file {name} from: {baseUrl}.");

            await Task.CompletedTask;
        }

        private static void CreateDirectoryWhenNotExist(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }
    }
}