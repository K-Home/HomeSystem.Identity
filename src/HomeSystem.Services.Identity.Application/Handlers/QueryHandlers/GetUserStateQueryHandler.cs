using HomeSystem.Services.Identity.Application.Messages.Queries;
using HomeSystem.Services.Identity.Application.Services.Base;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Application.Handlers.QueryHandlers
{
    public class GetUserStateQueryHandler : IRequestHandler<GetUserStateQuery, string>
    {
        private readonly IUserService _userService;

        public GetUserStateQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        
        public async Task<string> Handle(GetUserStateQuery query, CancellationToken cancellationToken)
        {
            var state = await _userService.GetStateAsync(query.Id);

            return state;
        }
    }
}