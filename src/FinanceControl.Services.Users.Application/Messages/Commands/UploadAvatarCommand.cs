using System;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class UploadAvatarCommand : IFileUploadCommand
    {
        public Request Request { get; }
        public Guid UserId { get; }
        public string Filename { get; }
        public string FileContentType { get; }
        public string FileBase64 { get; }

        [JsonConstructor]
        public UploadAvatarCommand(Guid userId, string filename, 
            string fileContentType, string fileBase64)
        {
            Request = Request.New<UploadAvatarCommand>();
            UserId = userId;
            Filename = filename;
            FileContentType = fileContentType;
            FileBase64 = fileBase64;
        }
    }
}