using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Exceptions;
using FinanceControl.Services.Users.Application.Messages.Commands;
using FinanceControl.Services.Users.Application.Messages.DomainEvents;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure.Files.Base;
using FinanceControl.Services.Users.Infrastructure.Handlers;
using FinanceControl.Services.Users.Infrastructure.MediatR.Bus;
using MediatR;

namespace FinanceControl.Services.Users.Application.Handlers.CommandHandlers
{
    internal class UploadAvatarCommandHandler : AsyncRequestHandler<UploadAvatarCommand>
    {
        private readonly IHandler _handler;
        private readonly IMediatRBus _mediatRBus;
        private readonly IAvatarService _avatarService;
        private readonly IFileResolver _fileResolver;

        public UploadAvatarCommandHandler(IHandler handler, IMediatRBus mediatRBus,
            IAvatarService avatarService, IFileResolver fileResolver)
        {
            _handler = handler.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _avatarService = avatarService.CheckIfNotEmpty();
            _fileResolver = fileResolver.CheckIfNotEmpty();

        }

        protected override async Task Handle(UploadAvatarCommand command, CancellationToken cancellationToken)
        {
            string avatarUrl;

            await _handler
                .Run(async () =>
                {
                    var avatar = _fileResolver.FromBase64(command.FileBase64, command.Filename,
                        command.FileContentType);

                    if (avatar.HasNoValue())
                    {
                        throw new ServiceException(Codes.AvatarIsInvalid,
                            $"Avatar with filename: {command.Filename} is invalid.");
                    }

                    await _avatarService.AddOrUpdateAsync(command.UserId, avatar);
                })
                .OnSuccess(async () =>
                {
                    avatarUrl = await _avatarService.GetUrlAsync(command.UserId);
                    await _mediatRBus.PublishAsync(new AvatarUploadedDomainEvent(command.Request.Id, command.UserId,
                        avatarUrl), cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(new UploadAvatarRejectedDomainEvent(command.Request.Id,
                        command.UserId, customException.Code, customException.Message), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error($"Error occured when uploading avatar for user with id: {command.UserId}");
                    await _mediatRBus.PublishAsync(new UploadAvatarRejectedDomainEvent(command.Request.Id,
                        command.UserId, Codes.Error, exception.Message), cancellationToken);
                })
                .ExecuteAsync();
        }
    }
}
