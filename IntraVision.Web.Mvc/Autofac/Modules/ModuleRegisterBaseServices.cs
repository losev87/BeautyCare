using Autofac;
using IntraVision.Web.Mvc.Services;

namespace IntraVision.Web.Mvc.Autofac.Modules
{
    public class ModuleRegisterBaseServices : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(BaseService<,,,,>)).As(typeof(IBaseService<,,,,>)).InstancePerRequest();

            builder.RegisterGeneric(typeof(FilterableBaseService<,,,,,>)).As(typeof(IFilterableBaseService<,,,,,>)).InstancePerRequest();
        }
    }
}
