using System;

namespace XSecure.Services.Users.Infrastructure.Authorization
{
    public class JwtSession : JwtBasic
    {
        public Guid SessionId { get; set; }
        public string Key { get; set; }
    }
}