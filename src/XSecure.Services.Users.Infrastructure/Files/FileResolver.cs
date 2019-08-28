using Serilog;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using XSecure.Services.Users.Domain.Extensions;
using XSecure.Services.Users.Infrastructure.Files.Base;

namespace XSecure.Services.Users.Infrastructure.Files
{
    public class FileResolver : IFileResolver
    {
        private readonly ILogger _logger;

        public FileResolver(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public File FromBase64(string base64, string name, string contentType)
        {
            if (base64.IsEmpty())
                return new File();
            if (name.IsEmpty())
                return new File();
            if (contentType.IsEmpty())
                return new File();

            var startIndex = 0;
            if (base64.Contains(","))
                startIndex = base64.IndexOf(",", StringComparison.CurrentCultureIgnoreCase) + 1;

            var base64String = base64.Substring(startIndex);
            var bytes = Convert.FromBase64String(base64String);

            return File.Create(name, contentType, bytes);
        }

        public async Task<Stream> FromUrlAsync(string url)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var result = await httpClient.GetAsync(url);
                    var stream = await result.Content.ReadAsStreamAsync();

                    return stream;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"There was an error when trying to get a stream from URL: {url}.");

                return Stream.Null;
            }
        }
    }
}