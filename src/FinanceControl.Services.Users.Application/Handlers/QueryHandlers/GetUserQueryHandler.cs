using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceControl.Services.Users.Application.Dtos;
using FinanceControl.Services.Users.Application.Messages.Queries;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain.Aggregates;
using MediatR;

namespace FinanceControl.Services.Users.Application.Handlers.QueryHandlers
{
    internal sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetUserQueryHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<UserDto> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            var user = await _userService.GetAsync(query.UserId);
            var mappedUser = _mapper.Map<User, UserDto>(user);

            return mappedUser;
        }
    }
}