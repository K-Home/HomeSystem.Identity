using FluentValidation;
using Microsoft.Extensions.Logging;
using XSecure.Services.Users.Application.Messages.Commands;

namespace XSecure.Services.Users.Application.Validations
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator(ILogger<SignUpCommandValidator> logger)
        {
            RuleFor(sg => sg.UserName).NotEmpty().WithMessage("Username is empty");
            RuleFor(sg => sg.Email).NotEmpty().WithMessage("Email is empty");
            RuleFor(sg => sg.Password).NotEmpty().WithMessage("Password is empty");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}