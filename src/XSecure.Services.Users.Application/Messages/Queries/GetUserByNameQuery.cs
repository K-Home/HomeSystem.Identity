using XSecure.Services.Users.Application.Dtos;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.Queries
{
    public class GetUserByNameQuery : IQuery<UserDto>
    {
        public string Name { get; set; }
    }
}
