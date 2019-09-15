namespace FinanceControl.Services.Users.Infrastructure.Authorization
{
    public class JwtTokenSettings
    {
        public string SecretKey { get; set; }
        public int ExpiryDays { get; set; }
        public string Issuer { get; set; }
        public bool ValidateIssuer { get; set; }
        public string ValidAudience { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
    }
}