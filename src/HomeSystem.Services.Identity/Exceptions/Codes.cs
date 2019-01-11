namespace HomeSystem.Services.Identity.Exceptions
{
    public static class Codes
    {
        public const string RefreshTokenAlreadyRevoked = "refresh_token_already_revoked";

        public const string InvalidFirstName = "invalid_first_name";
        public const string InvalidLastName = "invalid_first_name";
        public const string InvalidEmail = "invalid_email";
        public const string InvalidPassword = "invalid_password";
        public const string InvalidRole = "invalid_role";
        
        public const string FirstNameNotProvided = "first_name_not_provided";
        public const string LastNameNotProvided = "last_name_not_provided";
        public const string EmailNotProvided = "email_not_provided";
        public const string RoleNotProvided = "role_not_provided";
    }
}