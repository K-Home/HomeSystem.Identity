using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.Queries
{
    public class GetNameAvailablityQuery : IQuery<bool>
    {
        public string Name { get; set; }
    }
}
