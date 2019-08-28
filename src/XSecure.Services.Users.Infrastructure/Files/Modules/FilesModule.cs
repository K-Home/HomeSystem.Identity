using Autofac;
using XSecure.Services.Users.Infrastructure.Files.Base;

namespace XSecure.Services.Users.Infrastructure.Files.Modules
{
    public class FilesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileValidator>()
                .As<IFileValidator>()
                .SingleInstance();

            builder.RegisterType<FileResolver>()
                .As<IFileResolver>()
                .SingleInstance();

            builder.RegisterType<ImageService>()
                .As<IImageService>()
                .SingleInstance();

            builder.RegisterType<FileHandler>()
                .As<IFileHandler>()
                .SingleInstance();
        }
    }
}
