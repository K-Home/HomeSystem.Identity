using System;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Application.Exceptions;
using FinanceControl.Services.Users.Domain;
using FinanceControl.Services.Users.Domain.Aggregates;
using FinanceControl.Services.Users.Domain.Repositories;

namespace FinanceControl.Services.Users.Application.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrThrowAsync(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetByUserIdAsync(id);

            if (user == null)
            {
                throw new ServiceException(Codes.UserNotFound,
                    $"User with id: '{id}' does not exist!");
            }

            return user;
        }
    }
}