using System;

namespace HomeSystem.Services.Identity.Infrastructure.Authorization
{
    public class JwtSession : JwtBasic
    {
        public Guid SessionId { get; set; }
        public string Key { get; set; }
    }
}