using System.Runtime.Serialization;

namespace FinanceControl.Services.Users.Infrastructure.Messages
{
    public interface IFileUploadIntegrationCommand : IAuthenticatedIntegrationCommand
    {
        [DataMember]
        string Name { get; set; }

        [DataMember]
        string ContentType { get; set; }

        [DataMember]
        string FileBase64 { get; set; }
    }
}