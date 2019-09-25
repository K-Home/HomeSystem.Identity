using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Messages.Queries;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain.Extensions;
using MediatR;

namespace FinanceControl.Services.Users.Application.Handlers.QueryHandlers
{
    internal class GetUserStateQueryHandler : IRequestHandler<GetUserStateQuery, string>
    {
        private readonly IUserService _userService;

        public GetUserStateQueryHandler(IUserService userService)
        {
            _userService = userService.CheckIfNotEmpty();
        }

        public async Task<string> Handle(GetUserStateQuery query, CancellationToken cancellationToken)
        {
            var state = await _userService.GetStateAsync(query.Id);

            return state;
        }
    }
}