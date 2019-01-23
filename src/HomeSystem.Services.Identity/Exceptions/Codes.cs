namespace HomeSystem.Services.Identity.Exceptions
{
    public static class Codes
    {
        public const string RefreshTokenAlreadyRevoked = "refresh_token_already_revoked";

        public const string InvalidFirstName = "invalid_first_name";
        public const string InvalidLastName = "invalid_first_name";
        public const string InvalidEmail = "invalid_email";
        public const string InvalidPassword = "invalid_password";
        public const string InvalidPhoneNumber = "invalid_phone_number";
        public const string InvalidRole = "invalid_role";
        public const string InvalidCredentials = "invalid_credentials";
        public const string InvalidCurrentPassword = "invalid_current_password";
        public const string InvalidSecuredOperation = "invalid_secured_operation";

        public const string FirstNameNotProvided = "first_name_not_provided";
        public const string LastNameNotProvided = "last_name_not_provided";
        public const string EmailNotProvided = "email_not_provided";
        public const string RoleNotProvided = "role_not_provided";
        public const string AddressNotProvided = "address_not_provided";

        public const string UserNotFound = "user_not_found";
        public const string RefreshTokenNotFound = "refresh_token_not_found";
        
        public const string EmailInUse = "email_in_use";
        
        public const string SessionAlreadyDestroyed = "session_already_destroyed";
        public const string SessionAlreadyRefreshed = "session_already_refreshed";
    }
}