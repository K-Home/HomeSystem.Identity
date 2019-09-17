using System;
using System.Collections.Generic;
using System.IO;
using FinanceControl.Services.Users.Infrastructure.Files.Base;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace FinanceControl.Services.Users.Infrastructure.Files
{
    public class ImageService : IImageService
    {
        private static readonly double SmallSize = 200;
        private static readonly double MediumSize = 640;
        private static readonly double BigSize = 1200;

        private readonly ILogger _logger;

        public ImageService(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public File ProcessImage(File file, double size)
        {
            using (var originalImage = Image.Load(file.Bytes))
            {
                return File.Create(file.Name, file.ContentType,
                    ScaleImage(originalImage, size));
            }
        }

        public IDictionary<string, File> ProcessImage(File file)
        {
            _logger.Information($"Processing image: '{file.Name}', content type: '{file.ContentType}', " +
                                $"size: {file.SizeBytes} bytes.");

            using (var originalImage = Image.Load(file.Bytes))
            {
                var bigImage = ScaleImage(originalImage, BigSize);
                var mediumImage = ScaleImage(originalImage, MediumSize);
                var smallImage = ScaleImage(originalImage, SmallSize);
                var dictionary = new Dictionary<string, File>
                {
                    {"small", File.Create(file.Name, file.ContentType, smallImage)},
                    {"medium", File.Create(file.Name, file.ContentType, mediumImage)},
                    {"big", File.Create(file.Name, file.ContentType, bigImage)}
                };

                return dictionary;
            }
        }

        private byte[] ScaleImage(Image<Rgba32> image, double maxSize)
        {
            var ratioX = maxSize / image.Width;
            var ratioY = maxSize / image.Height;
            var ratio = Math.Min(ratioX, ratioY);
            var newWidth = (int) (image.Width * ratio);
            var newHeight = (int) (image.Height * ratio);

            using (var stream = new MemoryStream())
            {
                image.Mutate(i => i.Resize(newWidth, newHeight));
                image.SaveAsJpeg(stream);

                return stream.ToArray();
            }
        }
    }
}