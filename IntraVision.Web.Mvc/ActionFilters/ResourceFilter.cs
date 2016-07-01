using System.Web.Mvc;

namespace IntraVision.Web.Mvc
{
    public class ResourceFilter : ActionFilterAttribute
    {
        public string[] Scripts {get;set;}
        public string[] Styles{ get;set;}

        public ResourceFilter() { }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {           
            if (filterContext.Controller != null)
            {
                if (Scripts != null)
                    filterContext.Controller.ViewData["Scripts"] = Scripts;
                if (Styles != null)
                    filterContext.Controller.ViewData["Styles"] = Styles;
            }
        }
    }
}
