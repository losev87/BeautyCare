using Autofac;
using Autofac.Integration.WebApi;

namespace IntraVision.Web.Mvc.Autofac.Modules
{
    public class ModuleRegisterApiController<TController> : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(typeof(TController).Assembly).InstancePerRequest();
        }
    }
}
