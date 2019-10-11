using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Messages.Commands;
using FinanceControl.Services.Users.Application.Messages.DomainEvents;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Handlers;
using FinanceControl.Services.Users.Infrastructure.MediatR.Bus;
using MediatR;

namespace FinanceControl.Services.Users.Application.Handlers.CommandHandlers
{
    internal sealed class RemoveAvatarCommandHandler : AsyncRequestHandler<RemoveAvatarCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IAvatarService _avatarService;

        public RemoveAvatarCommandHandler(IHandler handler, IMediatRBus mediatRBus, IAvatarService avatarService)
        {
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _avatarService = avatarService.CheckIfNotEmpty();
        }

        protected override async Task Handle(RemoveAvatarCommand command, CancellationToken cancellationToken)
        {
            await _handler
                .Run(async () =>
                {
                    await _avatarService.RemoveAsync(command.UserId);
                    await _avatarService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                {
                    await _mediatRBus.PublishAsync(new AvatarRemovedDomainEvent(command.Request.Id, command.UserId),
                        cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(new RemoveAvatarRejectedDomainEvent(command.Request.Id, command
                        .UserId, customException.Message, customException.Code), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error($"Error when removing avatar for user with id: {command.UserId}", exception);
                    await _mediatRBus.PublishAsync(new RemoveAvatarRejectedDomainEvent(command.Request.Id, command
                        .UserId, exception.Message, Codes.Error), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}