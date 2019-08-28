using MediatR;
using System.Threading;
using System.Threading.Tasks;
using XSecure.Services.Users.Application.Messages.Queries;
using XSecure.Services.Users.Application.Services.Base;

namespace XSecure.Services.Users.Application.Handlers.QueryHandlers
{
    public class IsNameAvailableQueryHandler : IRequestHandler<GetNameAvailablityQuery, bool>
    {
        private readonly IUserService _userService;

        public IsNameAvailableQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(GetNameAvailablityQuery query, CancellationToken cancellationToken)
        {
            var available = await _userService.IsNameAvailableAsync(query.Name);

            return available;
        }
    }
}