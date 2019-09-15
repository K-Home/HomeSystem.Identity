using Autofac;
using FinanceControl.Services.Users.Application.Services;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain.Services;
using FinanceControl.Services.Users.Infrastructure.Handlers;

namespace FinanceControl.Services.Users.Application.Modules
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