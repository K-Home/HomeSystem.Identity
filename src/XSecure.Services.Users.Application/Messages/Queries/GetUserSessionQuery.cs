using System;
using XSecure.Services.Users.Application.Dtos;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.Queries
{
    public class GetUserSessionQuery : IQuery<UserSessionDto>
    {
        public Guid Id { get; set; }
    }
}
