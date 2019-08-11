using Autofac;
using HomeSystem.Services.Identity.Application.Services;
using HomeSystem.Services.Identity.Application.Services.Base;
using HomeSystem.Services.Identity.Domain.Services;
using HomeSystem.Services.Identity.Infrastructure.Handlers;

namespace HomeSystem.Services.Identity.Application.Modules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<AuthenticationService>()
                .As<IAuthenticationService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<AvatarService>()
                .As<IAvatarService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<PasswordService>()
                .As<IPasswordService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<OneTimeSecuredOperationService>()
                .As<IOneTimeSecuredOperationService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<Encrypter>()
                .As<IEncrypter>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<Handler>()
                .As<IHandler>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
