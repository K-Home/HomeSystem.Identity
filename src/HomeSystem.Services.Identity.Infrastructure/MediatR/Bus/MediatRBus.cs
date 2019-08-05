using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HomeSystem.Services.Identity.Infrastructure.MediatR.Bus
{
    public class MediatRBus : IMediatRBus
    {
        private readonly IMediator _mediator;

        public MediatRBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Send<TCommand>(TCommand command) where TCommand : IRequest
        {
            await _mediator.Send(command);
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : INotification
        {
            await _mediator.Publish(@event);
        }
    }
}
