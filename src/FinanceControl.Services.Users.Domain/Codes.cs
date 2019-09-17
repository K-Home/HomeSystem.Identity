namespace FinanceControl.Services.Users.Domain
{
    public static class Codes
    {
        public static readonly string Error = "error";
        
        public static readonly string FirstNameIsInvalid = "first_name_is_invalid";
        public static readonly string LastNameIsInvalid = "last_name_is_invalid";
        public static readonly string EmailIsInvalid = "email_is_invalid";
        public static readonly string PasswordIsInvalid = "password_is_invalid";
        public static readonly string PhoneNumberIsInvalid = "phone_number_is_invalid";
        public static readonly string RoleIsInvalid = "role_is_invalid";
        public static readonly string CredentialsAreInvalid = "credentials_are_invalid";
        public static readonly string CurrentPasswordIsInvalid = "current_password_is_invalid";
        public static readonly string SecuredOperationIsInvalid = "secured_operation_is_invalid";
        public static readonly string SessionKeyIsInvalid = "session_key_is_invalid";
        public static readonly string FileIsInvalid = "file_is_invalid";

        public static readonly string FirstNameNotProvided = "first_name_not_provided";
        public static readonly string LastNameNotProvided = "last_name_not_provided";
        public static readonly string EmailNotProvided = "email_not_provided";
        public static readonly string AddressNotProvided = "address_not_provided";
        public static readonly string UserNotLocked = "user_not_locked";

        public static readonly string UserNotFound = "user_not_found";

        public static readonly string OperationNotFound = "operation_not_found";
        public static readonly string SessionNotFound = "session_not_found";

        public static readonly string EmailInUse = "email_in_use";
        public static readonly string UserIdInUse = "user_id_in_use";
        public static readonly string UserNameInUse = "user_name_in_use";

        public static readonly string InactiveUser = "inactive_user";
        public static readonly string OwnerCannotBeLocked = "owner_cannot_be_locked";

        public static readonly string UserNameAlreadySet = "user_name_already_set";
        public static readonly string SessionAlreadyDestroyed = "session_already_destroyed";
        public static readonly string SessionAlreadyRefreshed = "session_already_refreshed";
        public static readonly string UserAlreadyLocked = "user_already_locked";
        public static readonly string UserAlreadyActive = "user_already_active";
        public static readonly string UserAlreadyUnconfirmed = "user_already_unconfirmed";
        public static readonly string UserAlreadyDeleted = "user_already_deleted";
    }
}