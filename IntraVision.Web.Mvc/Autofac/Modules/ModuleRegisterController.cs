using Autofac;
using Autofac.Integration.Mvc;

namespace IntraVision.Web.Mvc.Autofac.Modules
{
    public class ModuleRegisterController<TController> : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(TController).Assembly);
        }
    }
}
