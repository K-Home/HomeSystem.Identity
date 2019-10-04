namespace FinanceControl.Services.Users.Domain.Enumerations
{
    public static class Roles
    {
        public static string User => "user";
        public static string Moderator => "moderator";
        public static string Administrator => "administrator";
        public static string Owner => "owner";

        public static bool IsValid(string role)
        {
            return role == User || role == Moderator || role == Administrator || role == Owner;
        }
    }
}