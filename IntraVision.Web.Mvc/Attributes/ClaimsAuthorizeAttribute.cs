using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var authorize = true;

            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var controllerClaims = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(ClaimsAttribute), true).OfType<ClaimsAttribute>();
            var actionClaims = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ClaimsAttribute), true).OfType<ClaimsAttribute>();

            if (controllerClaims.Any() || actionClaims.Any())
            {
                var user = HttpContext.Current.User as System.Security.Claims.ClaimsPrincipal;
                if (user != null && user.Identity.IsAuthenticated)
                {
                    var controllerClaimsAuthorize = controllerClaims.All(c => user.HasClaim(c.Claim.Key, c.Claim.Value));
                    var actionClaimsAuthorize = actionClaims.All(c => user.HasClaim(c.Claim.Key, c.Claim.Value));

                    authorize = controllerClaimsAuthorize && actionClaimsAuthorize;
                }
            }

            if (authorize)
                base.OnAuthorization(filterContext);
            else
                HandleUnauthorizedRequest(filterContext);
        }
    }
}
