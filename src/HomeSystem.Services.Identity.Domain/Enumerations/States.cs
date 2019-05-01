using HomeSystem.Services.Identity.Domain.Types;

namespace HomeSystem.Services.Identity.Domain.Enumerations
{
    public class States : Enumeration
    {
        public static States Inactive = new InactiveState();
        public static States Incomplete = new IncompleteState();
        public static States Unconfirmed = new UnconfirmedState();
        public static States Active = new ActiveState();
        public static States Locked = new LockedState();
        public static States Deleted = new DeletedState();

        public States(int id, string name)
            : base(id, name)
        {
        }

        private class InactiveState : States
        {
            public InactiveState() : base(1, "inactive") { }
        }

        private class IncompleteState : States
        {
            public IncompleteState() : base(2, "incomplete") { }
        }

        private class UnconfirmedState : States
        {
            public UnconfirmedState() : base(3, "unconfirmed") { }
        }

        private class ActiveState : States
        {
            public ActiveState() : base(4, "active") { }
        }

        private class LockedState : States
        {
            public LockedState() : base(5, "locked") { }
        }

        private class DeletedState : States
        {
            public DeletedState() : base(6, "deleted") { }
        }
    }
}