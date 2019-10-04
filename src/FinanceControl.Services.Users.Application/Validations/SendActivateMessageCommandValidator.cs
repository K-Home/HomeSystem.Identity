using FinanceControl.Services.Users.Application.Messages.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Services.Users.Application.Validations
{
    internal class SendActivateMessageCommandValidator : AbstractValidator<SendActivateAccountMessageCommand>
    {
        public SendActivateMessageCommandValidator(ILogger<SendActivateMessageCommandValidator> logger)
        {
            RuleFor(so => so.Request.Id).NotEmpty().WithMessage("RequestId is empty.");
            RuleFor(so => so.Request.Resource).NotEmpty().WithMessage("Resource from request or empty.");
            RuleFor(so => so.Email).EmailAddress().WithMessage("Email is invalid.");
            RuleFor(so => so.Username).NotEmpty().WithMessage("Username is empty.");
            RuleFor(so => so.UserId).NotEmpty().WithMessage("UserId is empty.");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}