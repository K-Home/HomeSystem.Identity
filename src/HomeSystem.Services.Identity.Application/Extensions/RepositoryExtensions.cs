using HomeSystem.Services.Identity.Application.Exceptions;
using HomeSystem.Services.Identity.Domain;
using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace HomeSystem.Services.Identity.Application.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrThrowAsync(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetByUserIdAsync(id);

            if (user == null)
                throw new ServiceException(Codes.UserNotFound,
                    $"User with id: '{id}' does not exist!");

            return user;
        }
    }
}