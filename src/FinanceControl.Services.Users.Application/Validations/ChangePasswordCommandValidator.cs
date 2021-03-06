﻿using FinanceControl.Services.Users.Application.Messages.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Services.Users.Application.Validations
{
    internal sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator(ILogger<ChangePasswordCommandValidator> logger)
        {
            RuleFor(so => so.Request.Id).NotEmpty().WithMessage("RequestId is empty.");
            RuleFor(so => so.Request.Resource).NotEmpty().WithMessage("Resource from request or empty.");
            RuleFor(so => so.UserId).NotEmpty().WithMessage("UserId is invalid.");
            RuleFor(so => so.NewPassword).NotEmpty().WithMessage("NewPassword is empty.");
            RuleFor(so => so.CurrentPassword).NotEmpty().WithMessage("CurrentPassword is empty.");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}