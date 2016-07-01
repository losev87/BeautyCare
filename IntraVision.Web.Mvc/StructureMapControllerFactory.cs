using System;
using System.Web.Mvc;
using IntraVision.Web.Mvc.Autofac;
using System.Web.Routing;
namespace IntraVision.Web.Mvc
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) return null;

            try
            {
                return GetInLifetimeScope.Instance(controllerType) as Controller;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Error: StructureMapControllerFactory.GetControllerInstance");
                throw;
            }
        }
    }
}
