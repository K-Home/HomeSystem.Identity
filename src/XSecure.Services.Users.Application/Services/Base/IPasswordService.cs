using System;
using System.Threading.Tasks;

namespace XSecure.Services.Users.Application.Services.Base
{
    public interface IPasswordService
    {
        Task ChangeAsync(Guid userId, string currentPassword, string newPassword);
        Task ResetAsync(Guid operationId, string email);
        Task SetNewAsync(string email, string token, string password);
    }
}