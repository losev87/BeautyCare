using System.Linq;
using Autofac;
using IntraVision.Core;
using IntraVision.Web.Mvc.Controls;

namespace IntraVision.Web.Mvc.Autofac.Modules
{
    public class ModuleRegisterGridModel<TGrid> : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var types = typeof(TGrid).Assembly.GetTypes()
                .Where(t => t.IsGenericTypeOf(typeof(GridModel<>))).ToArray();

            builder.RegisterTypes(types).AsSelf().InstancePerRequest();
        }
    }
}
