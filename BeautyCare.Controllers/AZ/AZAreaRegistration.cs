using System.Web.Mvc;

namespace BeautyCare.Controllers.AZ
{
    public class AZAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AZ";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AZ_default",
                "AZ/{controller}/{action}/{id}",
                new { controller="User", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
