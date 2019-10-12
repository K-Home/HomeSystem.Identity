using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinanceControl.Services.Users.Application.Services.Base
{
    public interface IPasswordService
    {
        Task ChangeAsync(Guid userId, string currentPassword, string newPassword);
        Task ResetAsync(Guid operationId, string email);
        Task SetNewAsync(string email, string token, string password);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}