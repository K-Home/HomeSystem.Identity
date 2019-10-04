using System;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Api.Extensions;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain.Extensions;
using FinanceControl.Services.Users.Infrastructure;
using FinanceControl.Services.Users.Infrastructure.MediatR.Bus;
using FinanceControl.Services.Users.Infrastructure.Messages;
using FinanceControl.Services.Users.Infrastructure.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Services.Users.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        private static readonly string OperationHeader = "X-Operation";
        private static readonly string ResourceHeader = "X-Resource";
        private static readonly string PageLink = "page";

        private readonly IMediatRBus _mediatRBus;
        private readonly IAuthenticationService _authenticationService;
        private readonly AppOptions _settings;

        public BaseController(IMediatRBus mediatRBus,
            IAuthenticationService authenticationService, AppOptions settings)
        {
            _mediatRBus = mediatRBus.CheckIfNotEmpty();
            _authenticationService = authenticationService.CheckIfNotEmpty();
            _settings = settings.CheckIfNotEmpty();
        }

        protected async Task<IActionResult> HandleSessionRequestAsync<TCommand>(TCommand command)
            where TCommand : class, ISessionCommand
        {
            command.BindRequest(HttpContext);

            await _mediatRBus.SendAsync(command);

            var jwtSession = await _authenticationService.GetJwtSessionAsync(command.SessionId,
                command.Request.IpAddress, command.Request.UserAgent);

            return Ok(jwtSession);
        }

        protected async Task<IActionResult> HandleRequestWithToken<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            command.BindRequest(HttpContext);

            await CheckIfTokenIsValid(command.Request.IpAddress, command.Request.UserAgent);
            await _mediatRBus.SendAsync(command);

            return Accepted(command.Request.Id,
                FormatResourceString(_settings.ServiceName, command.Request.Resource));
        }

        protected async Task<IActionResult> HandleRequestAsync<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            command.BindRequest(HttpContext);

            await _mediatRBus.SendAsync(command);

            return Accepted(command.Request.Id,
                FormatResourceString(_settings.ServiceName, command.Request.Resource));
        }

        protected async Task<IActionResult> FetchRequestWithTokenAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
            where TResult : class
        {
            await CheckIfTokenIsValid();
            var result = await _mediatRBus.QueryAsync<IQuery<TResult>, TResult>(query);

            return HandleWithResult(result);
        }

        protected async Task<IActionResult> FetchRequestAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
            where TResult : class
        {
            var result = await _mediatRBus.QueryAsync<IQuery<TResult>, TResult>(query);

            return HandleWithResult(result);
        }

        private IActionResult HandleWithResult<TResult>(TResult result) where TResult : class
        {
            if (result.HasNoValue())
            {
                return NotFound();
            }

            if (!(result is PagedResult<TResult> pagedResult))
            {
                return Ok(result);
            }

            Response.Headers.Add("Link", GetLinkHeader(pagedResult));
            Response.Headers.Add("X-Total-Count", pagedResult.TotalNumberOfPages.ToString());

            return Ok(result);
        }

        private IActionResult Accepted(Guid requestId, string resource)
        {
            Response.Headers.Add(OperationHeader, $"operations/{requestId}");

            if (resource.IsNotEmpty())
            {
                Response.Headers.Add(ResourceHeader, resource);
            }

            return base.Accepted();
        }

        private async Task CheckIfTokenIsValid(string ipAddress, string userAgent)
        {
            var token = await GetTokenFromAuthHeader();

            await _authenticationService.CheckIpAddressOrDevice(ipAddress, userAgent, token);
        }

        private async Task CheckIfTokenIsValid()
        {
            var ipAddress = RequestExtensions.GetIpAddressFromHeader(HttpContext);
            var userAgent = RequestExtensions.GetUserAgent(HttpContext);
            var token = await GetTokenFromAuthHeader();

            await _authenticationService.CheckIpAddressOrDevice(ipAddress, userAgent, token);
        }

        private async Task<string> GetTokenFromAuthHeader()
        {
            var authHeader = HttpContext.Request.Headers["Authentication"].ToString();
            var headerValue = authHeader.IsEmpty() ? string.Empty : authHeader;
            var token = await _authenticationService.GetTokenFromHeader(headerValue);

            return token;
        }

        protected Guid UserId
            => string.IsNullOrWhiteSpace(User?.Identity?.Name) ? Guid.Empty : Guid.Parse(User.Identity.Name);

        private string GetLinkHeader<T>(PagedResult<T> result) where T : class
        {
            var first = GetPageLink(result.PageNumber, 1);
            var last = GetPageLink(result.PageNumber, result.TotalNumberOfPages);
            var prev = string.Empty;
            var next = string.Empty;

            if (result.PageNumber > 1 && result.PageNumber <= result.TotalNumberOfPages)
            {
                prev = GetPageLink(result.PageNumber, result.PageNumber - 1);
            }

            if (result.PageNumber < result.TotalNumberOfPages)
            {
                next = GetPageLink(result.PageNumber, result.PageNumber + 1);
            }

            return $"{FormatLink(next, "next")}{FormatLink(last, "last")}" +
                   $"{FormatLink(first, "first")}{FormatLink(prev, "prev")}";
        }

        private string GetPageLink(int currentPage, int page)
        {
            var path = Request.Path.HasValue ? Request.Path.ToString() : string.Empty;
            var queryString = Request.QueryString.HasValue ? Request.QueryString.ToString() : string.Empty;
            var conjunction = string.IsNullOrWhiteSpace(queryString) ? "?" : "&";
            var fullPath = $"{path}{queryString}";
            var pageArg = $"{PageLink}={page}";
            var link = fullPath.Contains($"{PageLink}=")
                ? fullPath.Replace($"{PageLink}={currentPage}", pageArg)
                : fullPath + $"{conjunction}{pageArg}";

            return link;
        }

        private static string FormatResourceString(string serviceName, string resource)
        {
            return serviceName.IsEmpty() || resource.IsEmpty() ? string.Empty : $"{serviceName}/{resource}";
        }

        private static string FormatLink(string path, string rel)
        {
            return path.IsEmpty() ? string.Empty : $"<{path}>; rel=\"{rel}\",";
        }
    }
}