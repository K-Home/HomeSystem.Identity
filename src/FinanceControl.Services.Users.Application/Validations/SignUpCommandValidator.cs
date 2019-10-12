using FinanceControl.Services.Users.Application.Messages.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Services.Users.Application.Validations
{
    internal sealed class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator(ILogger<SignUpCommandValidator> logger)
        {
            RuleFor(so => so.Request.Id).NotEmpty().WithMessage("RequestId is empty.");
            RuleFor(so => so.Request.Culture).NotEmpty().WithMessage("Culture from request is empty.");
            RuleFor(so => so.Request.Resource).NotEmpty().WithMessage("Resource from request is empty.");
            RuleFor(sg => sg.UserName).NotEmpty().WithMessage("Username is empty.");
            RuleFor(sg => sg.Email).EmailAddress().WithMessage("Email is invalid.");
            RuleFor(sg => sg.Password).NotEmpty().WithMessage("Password is empty.");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}