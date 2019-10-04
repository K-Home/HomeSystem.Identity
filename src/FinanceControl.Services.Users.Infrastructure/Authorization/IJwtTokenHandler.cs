using System;

namespace FinanceControl.Services.Users.Infrastructure.Authorization
{
    public interface IJwtTokenHandler
    {
        JwtDetails Parse(string token);
        JwtBasic Create(Guid userId, Guid sessionId, string role, 
            string state, string ipAddress, string userAgent);
        
        string GetFromAuthorizationHeader(string authorizationHeader);
    }
}