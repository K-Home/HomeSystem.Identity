namespace FinanceControl.Services.Users.Domain
{
    public static class Codes
    {
        public const string Error = "error";
        
        public const string FirstNameIsInvalid = "first_name_is_invalid";
        public const string LastNameIsInvalid = "last_name_is_invalid";
        public const string EmailIsInvalid = "email_is_invalid";
        public const string PasswordIsInvalid = "password_is_invalid";
        public const string PhoneNumberIsInvalid = "phone_number_is_invalid";
        public const string RoleIsInvalid = "role_is_invalid";
        public const string CredentialsAreInvalid = "credentials_are_invalid";
        public const string CurrentPasswordIsInvalid = "current_password_is_invalid";
        public const string SecuredOperationIsInvalid = "secured_operation_is_invalid";
        public const string SessionKeyIsInvalid = "session_key_is_invalid";
        public const string FileIsInvalid = "file_is_invalid";

        public const string FirstNameNotProvided = "first_name_not_provided";
        public const string LastNameNotProvided = "last_name_not_provided";
        public const string EmailNotProvided = "email_not_provided";
        public const string AddressNotProvided = "address_not_provided";
        public const string UserNotLocked = "user_not_locked";

        public const string UserNotFound = "user_not_found";

        public const string OperationNotFound = "operation_not_found";
        public const string SessionNotFound = "session_not_found";

        public const string EmailInUse = "email_in_use";
        public const string UserIdInUse = "user_id_in_use";
        public const string UserNameInUse = "user_name_in_use";

        public const string InactiveUser = "inactive_user";
        public const string OwnerCannotBeLocked = "owner_cannot_be_locked";

        public const string UserNameAlreadySet = "user_name_already_set";
        public const string SessionAlreadyDestroyed = "session_already_destroyed";
        public const string SessionAlreadyRefreshed = "session_already_refreshed";
        public const string UserAlreadyLocked = "user_already_locked";
        public const string UserAlreadyActive = "user_already_active";
        public const string UserAlreadyUnconfirmed = "user_already_unconfirmed";
        public const string UserAlreadyDeleted = "user_already_deleted";
    }
}