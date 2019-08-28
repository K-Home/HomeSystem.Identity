using System;
using XSecure.Services.Users.Application.Dtos;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.Queries
{
    public class GetUserQuery : IQuery<UserDto>
    {
        public Guid Id { get; set; }
    }
}
    