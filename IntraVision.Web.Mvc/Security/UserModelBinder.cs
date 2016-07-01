using System.Web.Mvc;

namespace IntraVision.Web.Mvc.Security
{
    public class UserModelBinder : IModelBinder, IFilteredModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return controllerContext.HttpContext.User as IMVCPrincipal;
        }

        public bool IsMatch(ModelBindingContext context)
        {
            return typeof(IMVCPrincipal).IsAssignableFrom(context.ModelType);
        }
    }
}
