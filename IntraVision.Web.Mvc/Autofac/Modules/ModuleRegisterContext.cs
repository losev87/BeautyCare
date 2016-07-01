using System.Data.Entity;
using Autofac;
using Autofac.Integration.Mvc;

namespace IntraVision.Web.Mvc.Autofac.Modules
{
    public class ModuleRegisterContext<TDbContext> : Module
        where TDbContext : DbContext
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TDbContext>().AsSelf().InstancePerHttpRequest();
        }
    }
}
