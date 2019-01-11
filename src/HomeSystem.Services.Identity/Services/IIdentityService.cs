using System;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Domain.Enumerations;
using KShared.Authentication.Tokens;

namespace HomeSystem.Services.Identity.Services
{
    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, string firstName, string lastName, string email, string password,
            string role = Role.User);

        Task<JsonWebToken> SignInAsync(string email, string password);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}