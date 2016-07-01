using System.Security.Claims;

namespace IntraVision.Web.Mvc
{
    public static class AuthenticationTicketIdentity
    {
        public static ClaimsIdentity Identity { get; private set; }

        public static void Set(ClaimsIdentity identity)
        {
            Identity = identity;
        }
    }
}
