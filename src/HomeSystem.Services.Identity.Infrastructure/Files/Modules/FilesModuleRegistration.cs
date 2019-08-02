using Autofac;
using HomeSystem.Services.Identity.Infrastructure.Files.Base;

namespace HomeSystem.Services.Identity.Infrastructure.Files.Modules
{
    public class FilesModuleRegistration
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
}
