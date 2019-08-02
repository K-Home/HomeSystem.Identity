using HomeSystem.Services.Identity.Infrastructure.Extensions;
using MediatR;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private static readonly ILogger Logger = Log.Logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Logger.Information("----- Handling command {CommandName} ({@Command})", request.GetGenericTypeName(), request);
            var response = await next();
            Logger.Information("----- Command {CommandName} handled - response: {@Response}", request.GetGenericTypeName(), response);

            return response;
        }
    }
}