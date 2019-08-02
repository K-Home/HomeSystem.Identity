using System;
using HomeSystem.Services.Identity.Infrastructure.Messages;

namespace HomeSystem.Services.Identity.Application.Messages.Queries
{
    public class GetUserState : IQuery<string>
    {
        public Guid Id { get; set; }
    }
}
