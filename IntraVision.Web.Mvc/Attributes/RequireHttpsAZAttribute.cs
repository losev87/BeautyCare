using System.Web.Configuration;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc.Attributes
{
    public class RequireHttpsAZAttribute : RequireHttpsAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //отключить обязательность ssl в AZ можно задав RequireHttpsAZ в web.config равным false
            var requireHttpsAz = WebConfigurationManager.AppSettings.Get("RequireHttpsAZ");

            if (string.IsNullOrEmpty(requireHttpsAz) || requireHttpsAz.ToLower() != "false")
            {
                base.OnAuthorization(filterContext);
            }
        }
    }
}
