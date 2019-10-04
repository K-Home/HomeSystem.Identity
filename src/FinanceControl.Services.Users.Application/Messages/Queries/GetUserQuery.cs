using System;
using FinanceControl.Services.Users.Application.Dtos;
using FinanceControl.Services.Users.Infrastructure.Messages;

namespace FinanceControl.Services.Users.Application.Messages.Queries
{
    public class GetUserQuery : IAuthenticatedQuery<UserDto>
    {
        public Guid UserId { get; set; }
    }
}