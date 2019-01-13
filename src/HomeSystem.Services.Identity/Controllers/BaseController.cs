using System;
using System.Threading.Tasks;
using KShared.CQRS.Dispatchers;
using KShared.CQRS.Messages;
using Microsoft.AspNetCore.Mvc;

namespace HomeSystem.Services.Identity.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public BaseController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        
        protected async Task<IActionResult> DispatchAsync<T>(T command,
            Guid? resourceId = null, string resource = "") where T : ICommand
        {
            await _dispatcher.SendAsync(command);

            return Ok();
        }
        
        protected bool IsAdmin
            => User.IsInRole("admin");

        protected Guid UserId
            => string.IsNullOrWhiteSpace(User?.Identity?.Name) ? 
                Guid.Empty : 
                Guid.Parse(User.Identity.Name);
        
        
    }
}