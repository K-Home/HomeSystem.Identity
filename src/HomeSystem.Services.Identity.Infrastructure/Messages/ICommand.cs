using System;
using MediatR;

namespace HomeSystem.Services.Identity.Infrastructure.Messages
{
    public interface ICommand : IRequest<bool>
    {
        Guid Id { get; }
        string Name { get; }
        DateTime When { get; }
    }
}
