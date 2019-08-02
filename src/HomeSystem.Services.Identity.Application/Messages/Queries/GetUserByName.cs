using HomeSystem.Services.Identity.Application.Dtos;
using HomeSystem.Services.Identity.Infrastructure.Messages;

namespace HomeSystem.Services.Identity.Application.Messages.Queries
{
    public class GetUserByName : IQuery<UserDto>
    {
        public string Name { get; set; }
    }
}
