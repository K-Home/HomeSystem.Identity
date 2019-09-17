using System;
using System.Threading.Tasks;
using FinanceControl.Services.Users.Infrastructure.Files;

namespace FinanceControl.Services.Users.Application.Services.Base
{
    public interface IAvatarService
    {
        Task<string> GetUrlAsync(Guid userId);
        Task AddOrUpdateAsync(Guid userId, File avatar);
        Task RemoveAsync(Guid userId);
    }
}