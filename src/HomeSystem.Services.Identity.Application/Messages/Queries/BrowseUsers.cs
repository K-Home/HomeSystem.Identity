using HomeSystem.Services.Identity.Application.Dtos;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using System.Collections.Generic;

namespace HomeSystem.Services.Identity.Application.Messages.Queries
{
    public class BrowseUsers : IQuery<IEnumerable<UserDto>>
    {
    }
}
