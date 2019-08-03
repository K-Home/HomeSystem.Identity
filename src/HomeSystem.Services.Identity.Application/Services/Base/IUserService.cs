using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSystem.Services.Identity.Domain.Aggregates;
using HomeSystem.Services.Identity.Domain.Enumerations;
using HomeSystem.Services.Identity.Domain.ValueObjects;

namespace HomeSystem.Services.Identity.Application.Services.Base
{
    public interface IUserService
    {
        Task<bool> IsNameAvailableAsync(string name);
        Task<User> GetAsync(Guid userId);
        Task<User> GetByNameAsync(string name);
        Task<User> GetByEmailAsync(string email);
        Task<string> GetStateAsync(Guid userId);
        Task<IEnumerable<User>> BrowseAsync();

        Task SignUpAsync(Guid userId, string email, Role role,
            string password = null, string externalUserId = null,
            bool activate = true, string name = null, string firstName = null, 
            string lastName = null);

        Task ChangeNameAsync(Guid userId, string name);
        Task ActivateAsync(string email, string token);
        Task LockAsync(Guid userId);
        Task UnlockAsync(Guid userId);
        Task DeleteAsync(Guid userId, bool soft);
        Task EnabledTwoFactorAuthorization(Guid userId);
        Task DisableTwoFactorAuthorization(Guid userId);
    }
}