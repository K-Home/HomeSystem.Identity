using HomeSystem.Services.Identity.Domain.Types;

namespace HomeSystem.Services.Identity.Domain.Enumerations
{
    public class Role : Enumeration
    {
        public static Role User = new UserRole();
        public static Role Editor = new EditorRole();
        public static Role Admin = new AdminRole();
        public static Role Owner = new OwnerRole();

        public Role(int id, string name)
            : base(id, name)
        {
        }

        private class UserRole : Role
        {
            public UserRole() : base(1, "user") { }
        }

        private class EditorRole : Role
        {
            public EditorRole() : base(2, "editor") { }
        }

        private class AdminRole : Role
        {
            public AdminRole() : base(3, "admin") { }
        }

        private class OwnerRole : Role
        {
            public OwnerRole() : base(4, "owner") { }
        }
    }
}