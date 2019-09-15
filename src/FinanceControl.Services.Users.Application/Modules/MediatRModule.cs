using System.Reflection;
using Autofac;
using FinanceControl.Services.Users.Application.Behaviors;
using FinanceControl.Services.Users.Application.Handlers.CommandHandlers;
using FinanceControl.Services.Users.Application.Handlers.DomainEventHandlers;
using FinanceControl.Services.Users.Infrastructure.MediatR.Bus;
using MediatR;
using Module = Autofac.Module;

namespace FinanceControl.Services.Users.Application.Modules
{
    public class MediatRModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(SignUpCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(SignedUpDomainEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            builder.RegisterType<MediatRBus>().As<IMediatRBus>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => componentContext.TryResolve(t, out var o) ? o : null;
            });

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
