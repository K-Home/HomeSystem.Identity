using FinanceControl.Services.Users.Application.Messages.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Services.Users.Application.Validations
{
    internal sealed class UploadAvatarCommandValidator : AbstractValidator<UploadAvatarCommand>
    {
        public UploadAvatarCommandValidator(ILogger<UploadAvatarCommandValidator> logger)
        {
            RuleFor(so => so.Request.Id).NotEmpty().WithMessage("RequestId is empty.");
            RuleFor(so => so.Request.Culture).NotEmpty().WithMessage("Culture from request is empty.");
            RuleFor(so => so.Request.Resource).NotEmpty().WithMessage("Resource from request is empty.");
            RuleFor(sg => sg.UserId).NotEmpty().WithMessage("UserId is empty.");
            RuleFor(sg => sg.Filename).NotEmpty().WithMessage("Filename is invalid.");
            RuleFor(sg => sg.FileBase64).NotEmpty().WithMessage("FileBase64 is empty.");
            RuleFor(sg => sg.FileContentType).NotEmpty().WithMessage("FileContentType is empty.");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}