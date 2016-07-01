using System.Security.Claims;
using System.Threading;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace IntraVision.Web.Mvc
{
    public class AuthenticationTicketIdentityAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var identity = AuthenticationTicketIdentity.Identity;
            if (identity != null)
            {
                var principal = new ClaimsPrincipal(identity);

                filterContext.HttpContext.User = principal;
                Thread.CurrentPrincipal = principal;
                filterContext.Principal = principal;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

        }
    }
}
