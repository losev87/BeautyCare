using System.Linq;
using System.Security.Principal;

namespace IntraVision.Web.Mvc.Security
{
    public class MVCPrincipal : IMVCPrincipal
    {
        private readonly string[] _roles;
        private readonly IMVCIdentity _identity;

        public string[] Roles { get { return _roles; } }

        public MVCPrincipal(IMVCIdentity identity, string[] roles)
        {
            _identity = identity;
            _roles = roles;
        }

        public IMVCIdentity Identity
        {
            get { return _identity; }
        }

        public bool IsInRole(string role)
        {
            if (_roles == null || _roles.Count() == 0) return false;
            return _roles.Contains(role);
        }

        IIdentity IPrincipal.Identity
        {
            get { return _identity; }
        }
    }
}
