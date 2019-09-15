using System;
using FinanceControl.Services.Users.Infrastructure.Files.Base;
using Serilog;
using SixLabors.ImageSharp;

namespace FinanceControl.Services.Users.Infrastructure.Files
{
    public class FileValidator : IFileValidator
    {
        private readonly ILogger _logger;

        public FileValidator(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool IsImage(File file)
        {
            try
            {
                using (var image = Image.Load(file.Bytes))
                {
                    return image.Width > 0 && image.Height > 0;
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Error while reading image from stream");

                return false;
            }
        }
    }
}