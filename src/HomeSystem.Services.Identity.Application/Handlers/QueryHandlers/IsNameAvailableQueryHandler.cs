using HomeSystem.Services.Identity.Application.Messages.Queries;
using HomeSystem.Services.Identity.Application.Services.Base;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Application.Handlers.QueryHandlers
{
    public class IsNameAvailableQueryHandler : IRequestHandler<GetNameAvailablity, bool>
    {
        private readonly IUserService _userService;

        public IsNameAvailableQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(GetNameAvailablity query, CancellationToken cancellationToken)
        {
            var available = await _userService.IsNameAvailableAsync(query.Name);

            return available;
        }
    }
}