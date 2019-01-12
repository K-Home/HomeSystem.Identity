namespace HomeSystem.Services.Identity.Domain.Enumerations
{
    public class Role
    {
        public const string User = "user";
        public const string Editor = "editor";
        public const string Admin = "admin";

        public static bool IsValid(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return false;
            }

            role = role.ToLowerInvariant();

            return role == User || role == Editor || role == Admin;
        }
    }
}