using FinanceControl.Services.Users.Application.Messages.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Services.Users.Application.Validations
{
    internal sealed class ActivateAccountCommandValidator : AbstractValidator<ActivateAccountCommand>
    {
        public ActivateAccountCommandValidator(ILogger<ActivateAccountCommandValidator> logger)
        {
            RuleFor(so => so.Request.Id).NotEmpty().WithMessage("RequestId is empty.");
            RuleFor(so => so.Request.Resource).NotEmpty().WithMessage("Resource from request or empty.");
            RuleFor(so => so.Email).EmailAddress().WithMessage("Email is invalid.");
            RuleFor(so => so.Token).NotEmpty().WithMessage("Token is empty.");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}