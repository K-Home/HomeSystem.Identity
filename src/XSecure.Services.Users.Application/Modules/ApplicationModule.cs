using Autofac;
using XSecure.Services.Users.Application.Services;
using XSecure.Services.Users.Application.Services.Base;
using XSecure.Services.Users.Domain.Services;
using XSecure.Services.Users.Infrastructure.Handlers;

namespace XSecure.Services.Users.Application.Modules
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
