using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceControl.Services.Users.Application.Dtos;
using FinanceControl.Services.Users.Application.Messages.Queries;
using FinanceControl.Services.Users.Application.Services.Base;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Domain.Extensions;
using MediatR;

namespace FinanceControl.Services.Users.Application.Handlers.QueryHandlers
{
    internal sealed class GetUserSessionQueryHandler : IRequestHandler<GetUserSessionQuery, UserSessionDto>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public GetUserSessionQueryHandler(IMapper mapper, IAuthenticationService authenticationService)
        {
            _mapper = mapper.CheckIfNotEmpty();
            _authenticationService = authenticationService.CheckIfNotEmpty();
        }

        public async Task<UserSessionDto> Handle(GetUserSessionQuery query, CancellationToken cancellationToken)
        {
            var session = await _authenticationService.GetSessionAsync(query.SessionId);
            var mappedSession = _mapper.Map<UserSession, UserSessionDto>(session);

            return mappedSession;
        }
    }
}