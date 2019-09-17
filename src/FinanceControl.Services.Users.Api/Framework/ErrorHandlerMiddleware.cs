using System;
using System.Net;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FinanceControl.Services.Users.Api.Framework
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await HandleErrorAsync(context, exception).ConfigureAwait(false);
            }
        }

        private static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            string errorCode;
            const HttpStatusCode statusCode = HttpStatusCode.BadRequest;
            
            switch (exception)
            {
                case FinanceControlException e:
                    errorCode = e.Code;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(exception));
            }

            var response = new {code = errorCode, message = exception.Message};
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}