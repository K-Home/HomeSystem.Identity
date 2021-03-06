using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Messages.Queries;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain.Extensions;
using MediatR;

namespace FinanceControl.Services.Users.Application.Handlers.QueryHandlers
{
    internal sealed class IsNameAvailableQueryHandler : IRequestHandler<GetNameAvailablityQuery, bool>
    {
        private readonly IUserService _userService;

        public IsNameAvailableQueryHandler(IUserService userService)
        {
            _userService = userService.CheckIfNotEmpty();
        }

        public async Task<bool> Handle(GetNameAvailablityQuery query, CancellationToken cancellationToken)
        {
            var available = await _userService.IsNameAvailableAsync(query.Name);

            return available;
        }
    }
}