using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace FinanceControl.Services.Users.Infrastructure.Authorization
{
    public class FinanceControlIdentity : ClaimsPrincipal
    {
        private readonly IEnumerable<Claim> _claims;
        public string Role { get; }
        public string State { get; }

        public override IEnumerable<Claim> Claims => _claims;

        public FinanceControlIdentity(string name, string role, string state,
            IEnumerable<Claim> claims)
            : base(new GenericIdentity(name))
        {
            Role = role;
            State = state;
            _claims = claims;
        }
    }
}