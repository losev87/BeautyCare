using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using IntraVision.Web.Mvc;
using IntraVision.Web.Mvc.Controls;

namespace BeautyCare.Configuration.Web
{
    public static class WebConfiguration
    {
        public static void Configure()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MvcHandler.DisableMvcResponseHeader = true;

            ModelMetadataProviders.Current = new IntraVisionModelMetadataProvider();
            ModelBinders.Binders.DefaultBinder = new SmartBinder(new[] { new GridOptionsModelBinder() });
        }
    }
}
