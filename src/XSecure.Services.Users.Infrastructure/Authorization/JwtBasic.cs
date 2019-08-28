namespace XSecure.Services.Users.Infrastructure.Authorization
{
    public class JwtBasic
    {
        public string Token { get; set; }
        public long Expires { get; set; }
    }
}