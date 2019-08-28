using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using XSecure.Services.Users.Application.Dtos;
using XSecure.Services.Users.Application.Messages.Queries;
using XSecure.Services.Users.Application.Services.Base;
using XSecure.Services.Users.Domain.Aggregates;

namespace XSecure.Services.Users.Application.Handlers.QueryHandlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
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
            var user = await _userService.GetAsync(query.Id);
            var mappedUser = _mapper.Map<User, UserDto>(user);

            return mappedUser;
        }
    }
}