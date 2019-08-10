namespace HomeSystem.Services.Identity.Domain.Enumerations
{
    public class OneTimeSecuredOperations 
    {
        public static string ResetPassword => "reset_password";
        public static string ActivateAccount => "activate_account";
        public static string LoginWithTwoFactor => "login_with_two_factor";
    }
}