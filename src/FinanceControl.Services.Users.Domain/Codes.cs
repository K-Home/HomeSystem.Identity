namespace FinanceControl.Services.Users.Domain
{
    public static class Codes
    {
        public static string Error = "error";

        public static string InvalidFirstName = "invalid_first_name";
        public static string InvalidLastName = "invalid_first_name";
        public static string InvalidEmail = "invalid_email";
        public static string InvalidPassword = "invalid_password";
        public static string InvalidPhoneNumber = "invalid_phone_number";
        public static string InvalidRole = "invalid_role";
        public static string InvalidCredentials = "invalid_credentials";
        public static string InvalidCurrentPassword = "invalid_current_password";
        public static string InvalidSecuredOperation = "invalid_secured_operation";
        public static string InvalidSessionKey = "invalid_session_key";
        public static string InvalidFile = "invalid_file";

        public static string FirstNameNotProvided = "first_name_not_provided";
        public static string LastNameNotProvided = "last_name_not_provided";
        public static string EmailNotProvided = "email_not_provided";
        public static string AddressNotProvided = "address_not_provided";
        public static string UserNotLocked = "user_not_locked";

        public static string UserNotFound = "user_not_found";

        public static string OperationNotFound = "operation_not_found";
        public static string SessionNotFound = "session_not_found";
        
        public static string EmailInUse = "email_in_use";
        public static string UserIdInUse = "user_id_in_use";
        public static string UserNameInUse = "user_name_in_use";

        public static string InactiveUser = "inactive_user";
        public static string OwnerCannotBeLocked = "owner_cannot_be_locked";
        
        public static string UserNameAlreadySet = "user_name_already_set";
        public static string SessionAlreadyDestroyed = "session_already_destroyed";
        public static string SessionAlreadyRefreshed = "session_already_refreshed";
        public static string UserAlreadyLocked = "user_already_locked";
        public static string UserAlreadyActive = "user_already_active";
        public static string UserAlreadyUnconfirmed = "user_already_unconfirmed";
        public static string UserAlreadyDeleted = "user_already_deleted";
    }
}