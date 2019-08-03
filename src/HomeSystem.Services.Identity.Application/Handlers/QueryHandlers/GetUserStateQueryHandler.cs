using System.Threading;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Application.Messages.Queries;
using HomeSystem.Services.Identity.Application.Services.Base;
using MediatR;

namespace HomeSystem.Services.Identity.Application.Handlers.QueryHandlers
{
    public class GetUserStateQueryHandler : IRequestHandler<GetUserState, string>
    {
        private readonly IUserService _userService;

        public GetUserStateQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        
        public async Task<string> Handle(GetUserState query, CancellationToken cancellationToken)
        {
            var state = await _userService.GetStateAsync(query.Id);

            return state;
        }
    }
}