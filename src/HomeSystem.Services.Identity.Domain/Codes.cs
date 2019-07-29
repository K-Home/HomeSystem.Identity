namespace HomeSystem.Services.Identity.Domain
{
    public static class Codes
    {
        public static string RefreshTokenAlreadyRevoked = "refresh_token_already_revoked";

        public static string InvalidFirstName = "invalid_first_name";
        public static string InvalidLastName = "invalid_first_name";
        public static string InvalidEmail = "invalid_email";
        public static string InvalidPassword = "invalid_password";
        public static string InvalidPhoneNumber = "invalid_phone_number";
        public static string InvalidRole = "invalid_role";
        public static string InvalidCredentials = "invalid_credentials";
        public static string InvalidCurrentPassword = "invalid_current_password";
        public static string InvalidSecuredOperation = "invalid_secured_operation";

        public static string FirstNameNotProvided = "first_name_not_provided";
        public static string LastNameNotProvided = "last_name_not_provided";
        public static string EmailNotProvided = "email_not_provided";
        public static string RoleNotProvided = "role_not_provided";
        public static string AddressNotProvided = "address_not_provided";
        public static string UserNotLocked = "user_not_locked";

        public static string UserNotFound = "user_not_found";
        public static string RefreshTokenNotFound = "refresh_token_not_found";
        
        public static string EmailInUse = "email_in_use";
        
        public static string SessionAlreadyDestroyed = "session_already_destroyed";
        public static string SessionAlreadyRefreshed = "session_already_refreshed";
        public static string UserAlreadyLocked = "user_already_locked";
        public static string UserAlreadyActive = "user_already_active";
        public static string UserAlreadyUnconfirmed = "user_already_unconfirmed";
        public static string UserAlreadyDeleted = "user_already_deleted";
    }
}