using HomeSystem.Services.Identity.Infrastructure.Messages;
using System;

namespace HomeSystem.Services.Identity.Application.Messages.Queries
{
    public class GetUserStateQuery : IQuery<string>
    {
        public Guid Id { get; set; }
    }
}
