using System;

namespace HomeSystem.Services.Identity.Infrastructure.Authorization
{
    public interface IJwtTokenHandler
    {
        JwtDetails Parse(string token);
        JwtBasic Create(Guid userId, string role, TimeSpan? expiry = null, string state = "active");
        string GetFromAuthorizationHeader(string authorizationHeader);
    }
}