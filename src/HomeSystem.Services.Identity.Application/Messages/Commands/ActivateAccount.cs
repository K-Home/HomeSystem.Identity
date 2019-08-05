using HomeSystem.Services.Identity.Infrastructure.Messages;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class ActivateAccount : ICommand
    {
        [DataMember]
        public Request Request { get; }

        [DataMember]
        public string Email { get; }

        [DataMember]
        public string Token { get; }

        [JsonConstructor]
        public ActivateAccount(Request request, string email, string token)
        {
            Request = request;
            Email = email;
            Token = token;
        }
    }
}
