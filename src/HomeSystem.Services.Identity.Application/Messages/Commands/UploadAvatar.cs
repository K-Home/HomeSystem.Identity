using System;
using HomeSystem.Services.Identity.Infrastructure.Messages;

namespace HomeSystem.Services.Identity.Application.Messages.Commands
{
    public class UploadAvatar : IAuthenticatedCommand
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public string Name { get; }
        public DateTime When { get; }

        public UploadAvatar(Guid id, Guid userId, string name, DateTime when)
        {
            Id = id;
            UserId = userId;
            Name = name;
            When = when;
        }
    }
}
