namespace FinanceControl.Services.Users.Domain.Enumerations
{
    public static class OneTimeSecuredOperations
    {
        public static string ResetPassword => "reset_password";
        public static string ActivateAccount => "activate_account";
        public static string LoginWithTwoFactor => "login_with_two_factor";
    }
}