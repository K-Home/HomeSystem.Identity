using System;
using System.Threading.Tasks;
using XSecure.Services.Users.Application.Exceptions;
using XSecure.Services.Users.Domain;
using XSecure.Services.Users.Domain.Aggregates;
using XSecure.Services.Users.Domain.Repositories;

namespace XSecure.Services.Users.Application.Extensions
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