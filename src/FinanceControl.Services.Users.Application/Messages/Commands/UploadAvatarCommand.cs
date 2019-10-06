using System;
using System.Runtime.Serialization;
using FinanceControl.Services.Users.Infrastructure.Messages;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Application.Messages.Commands
{
    public class UploadAvatarCommand : IFileUploadCommand
    {
        [DataMember]
        public Request Request { get; }

        [DataMember]
        public Guid UserId { get; }

        [DataMember]
        public string Filename { get; }

        [DataMember]
        public string FileContentType { get; }

        [DataMember]
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