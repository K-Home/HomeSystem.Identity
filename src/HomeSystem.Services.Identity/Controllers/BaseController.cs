using HomeSystem.Services.Identity.Domain.Extensions;
using HomeSystem.Services.Identity.Infrastructure;
using HomeSystem.Services.Identity.Infrastructure.MediatR.Bus;
using HomeSystem.Services.Identity.Infrastructure.Messages;
using HomeSystem.Services.Identity.Infrastructure.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Controllers
{
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        private static readonly string OperationHeader = "X-Operation";
        private static readonly string ResourceHeader = "X-Resource";
        private static readonly string PageLink = "page";

        private readonly IMediatRBus _mediatRBus;
        private readonly AppOptions _settings;

        public BaseController(IMediatRBus mediatRBus, AppOptions settings)
        {
            _mediatRBus = mediatRBus ?? throw new ArgumentNullException(nameof(mediatRBus));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        protected async Task<IActionResult> SendAsync<TCommand>(TCommand command, string endpoint = "") 
            where TCommand : class, ICommand
        {
            await _mediatRBus.SendAsync(command);

            return Accepted(command.Request.Id, $"{_settings.ServiceName}/{endpoint}");
        }

        protected async Task<IActionResult> QueryAsync<TQuery, TResult>(TQuery query) 
            where TQuery : IQuery<TResult> 
            where TResult : class
        {
            var result = await _mediatRBus.QueryAsync<IQuery<TResult>, TResult>(query);

            if (result == null)
                return NotFound();

            if (!(result is PagedResult<TResult> pagedResult))
                return Ok(result);

            Response.Headers.Add("Link", GetLinkHeader(pagedResult));
            Response.Headers.Add("X-Total-Count", pagedResult.TotalNumberOfPages.ToString());

            return Ok(result);
        }

        private IActionResult Accepted(Guid? requestId = null, string resource = "")
        {
            if (requestId != null)
                Response.Headers.Add(OperationHeader, $"operations/{requestId}");

            if (resource.IsNotEmpty())
                Response.Headers.Add(ResourceHeader, resource);

            return base.Accepted();
        }

        protected Guid UserId
            => string.IsNullOrWhiteSpace(User?.Identity?.Name) ?
                Guid.Empty :
                Guid.Parse(User.Identity.Name);

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
                : fullPath += $"{conjunction}{pageArg}";

            return link;
        }

        private static string FormatLink(string path, string rel)
            => string.IsNullOrWhiteSpace(path) ? string.Empty : $"<{path}>; rel=\"{rel}\",";
    }
}
