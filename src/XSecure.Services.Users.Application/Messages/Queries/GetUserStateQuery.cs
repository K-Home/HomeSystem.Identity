using System;
using XSecure.Services.Users.Infrastructure.Messages;

namespace XSecure.Services.Users.Application.Messages.Queries
{
    public class GetUserStateQuery : IQuery<string>
    {
        public Guid Id { get; set; }
    }
}
