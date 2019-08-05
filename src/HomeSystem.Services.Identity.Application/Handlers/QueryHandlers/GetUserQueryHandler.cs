using AutoMapper;
using HomeSystem.Services.Identity.Application.Dtos;
using HomeSystem.Services.Identity.Application.Messages.Queries;
using HomeSystem.Services.Identity.Application.Services.Base;
using HomeSystem.Services.Identity.Domain.Aggregates;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Application.Handlers.QueryHandlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUser, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetUserQueryHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        
        public async Task<UserDto> Handle(GetUser query, CancellationToken cancellationToken)
        {
            var user = await _userService.GetAsync(query.Id);
            var mappedUser = _mapper.Map<User, UserDto>(user);

            return mappedUser;
        }
    }
}