using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Domain.Aggregates;

namespace FinanceControl.Services.Users.Application.Services.Base
{
    public interface IUserService
    {
        Task<bool> IsNameAvailableAsync(string name);
        Task<User> GetAsync(Guid userId);
        Task<User> GetByNameAsync(string name);
        Task<User> GetByEmailAsync(string email);
        Task<string> GetStateAsync(Guid userId);
        Task<IEnumerable<User>> BrowseAsync();
        
        Task SignUpAsync(Guid userId, string email, string name, 
            string password, string culture);

        Task UpdateAsync(Guid userId, string userName, string firstName, string lastName, 
            string street, string city, string state, string country, string zipCode);
        
        Task SetPhoneNumber(Guid userId, string phoneNumber);
        Task ChangeNameAsync(Guid userId, string name);
        Task ActivateAsync(string email, string token);
        Task LockAsync(Guid userId);
        Task UnlockAsync(Guid userId);
        Task DeleteAsync(Guid userId, bool soft);
        Task EnabledTwoFactorAuthorization(Guid userId);
        Task DisableTwoFactorAuthorization(Guid userId);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}