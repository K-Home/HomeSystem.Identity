using System;
using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Messages.Commands;
using FinanceControl.Services.Users.Application.Messages.DomainEvents;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain;
using FinanceControl.Services.Users.Domain.Enumerations;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure;
using FinanceControl.Services.Users.Infrastructure.Handlers;
using FinanceControl.Services.Users.Infrastructure.MediatR.Bus;
using MediatR;

namespace FinanceControl.Services.Users.Application.Handlers.CommandHandlers
{
    public class
        SendActivationMessageWhenSignedUpCommandHandler : AsyncRequestHandler<
            SendActivateAccountMessageWhenSignedUpCommand>
    {
        private readonly IHandler _handler;
        private readonly IOneTimeSecuredOperationService _oneTimeSecuredOperationService;
        private readonly IMediatRBus _mediatRBus;
        private readonly AppOptions _appOptions;

        public SendActivationMessageWhenSignedUpCommandHandler(IHandler handler,
            IOneTimeSecuredOperationService oneTimeSecuredOperationService,
            IMediatRBus mediatRBus, AppOptions appOptions)
        {
            _handler = handler.CheckIfNotEmpty();
            _oneTimeSecuredOperationService = oneTimeSecuredOperationService.CheckIfNotEmpty();
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _appOptions = appOptions.CheckIfNotEmpty();
        }

        protected override async Task Handle(SendActivateAccountMessageWhenSignedUpCommand command,
            CancellationToken cancellationToken)
        {
            var operationId = Guid.NewGuid();

            await _handler
                .Run(async () =>
                {
                    await _oneTimeSecuredOperationService.CreateAsync(operationId,
                        OneTimeSecuredOperations.ActivateAccount, command.User.Id, DateTime.UtcNow.AddDays(7));

                    await _oneTimeSecuredOperationService.SaveChangesAsync(cancellationToken);
                })
                .OnSuccess(async () =>
                {
                    var operation = await _oneTimeSecuredOperationService.GetAsync(operationId);

                    await _mediatRBus.PublishAsync(new ActivateAccountSecuredOperationCreatedDomainEvent(
                        command.Request, command.User.Id, command.User.Username, command.User.Email, operation.Id,
                        $"Successfully created secured operation for user with id: {command.User.Id}.", operation.Token,
                        _appOptions.ActivationAccountUrl), cancellationToken);
                })
                .OnCustomError(async customException =>
                {
                    await _mediatRBus.PublishAsync(new CreateActivateAccountSecuredOperationRejectedDomainEvent(
                        command.Request.Id, command.User.Id, operationId,
                        $"Created secured operation for user with id: {command.User.Id} rejected, because custom exception was thrown",
                        customException.Message, customException.Code), cancellationToken);
                })
                .OnError(async (exception, logger) =>
                {
                    logger.Error("Error occured while creating a secured operation.", exception);
                    await _mediatRBus.PublishAsync(new CreateActivateAccountSecuredOperationRejectedDomainEvent(
                        command.Request.Id, command.User.Id, operationId,
                        $"Created secured operation for user with id: {command.User.Id} rejected, because exception was thrown",
                        exception.Message, Codes.Error), cancellationToken);
                }).ExecuteAsync();
        }
    }
}