using System;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc
{
    public class GenericControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            Type controllerType = GetControllerType(requestContext, controllerName);
            IController controller = GetControllerInstance(requestContext, controllerType);
            return controller;
        }

        protected override Type GetControllerType(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            var controllerType = base.GetControllerType(requestContext, controllerName);

            if (controllerType == null)
            {
                object controllerGenericType;
                requestContext.RouteData.Values.TryGetValue("controllerGenericType", out controllerGenericType);

                return controllerGenericType as Type;
            }

            return controllerType;
        }
    }
}
