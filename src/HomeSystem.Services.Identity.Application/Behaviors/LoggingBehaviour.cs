using HomeSystem.Services.Identity.Infrastructure.Extensions;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.Information("----- Handling command {CommandName} ({@Command})", request.GetGenericTypeName(), request);
            var response = await next();
            _logger.Information("----- Command {CommandName} handled - response: {@Response}", request.GetGenericTypeName(), response);

            return response;
        }
    }
}