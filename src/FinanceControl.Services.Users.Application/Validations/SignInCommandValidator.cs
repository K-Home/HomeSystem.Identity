using FinanceControl.Services.Users.Application.Messages.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Services.Users.Application.Validations
{
    internal class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator(ILogger<SignInCommandValidator> logger)
        {
            RuleFor(so => so.Request.Id).NotEmpty().WithMessage("RequestId is empty.");
            RuleFor(so => so.Request.Resource).NotEmpty().WithMessage("Resource from request or empty.");
            RuleFor(so => so.Email).EmailAddress().WithMessage("Email is invalid.");
            RuleFor(so => so.Password).NotEmpty().WithMessage("Password is empty.");
            RuleFor(so => so.SessionId).NotEmpty().WithMessage("SessionId is empty.");
            RuleFor(so => so.IpAddress).NotEmpty().WithMessage("IpAddress is empty.");
            RuleFor(so => so.UserAgent).NotEmpty().WithMessage("UserAgent is empty.");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}