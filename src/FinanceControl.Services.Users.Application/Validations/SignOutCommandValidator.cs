using FinanceControl.Services.Users.Application.Messages.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Services.Users.Application.Validations
{
    internal class SignOutCommandValidator : AbstractValidator<SignOutCommand>
    {
        public SignOutCommandValidator(ILogger<SignOutCommandValidator> logger)
        {
            RuleFor(so => so.Request.Id).NotEmpty().WithMessage("RequestId is empty.");
            RuleFor(so => so.Request.Resource).NotEmpty().WithMessage("Resource from request is empty.");
            RuleFor(so => so.SessionId).NotEmpty().WithMessage("SessionId is empty.");
            RuleFor(so => so.UserId).NotEmpty().WithMessage("UserId is empty.");
            
            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}