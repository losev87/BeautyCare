using System.Linq;
using Autofac;
using ClosedXML.Excel;
using IntraVision.Data;
using IntraVision.Repository;

namespace IntraVision.Web.Mvc.Autofac.Modules
{
    public class ModuleRegisterBaseRepository<TEntityContext, TContext> : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var typeContext = typeof (TContext);

            typeof(TEntityContext).Assembly.GetTypes()
                .Where(t => typeof(TEntityContext).IsAssignableFrom(t) &&
                            typeof(IEntityBase).IsAssignableFrom(t))
                .ForEach(t => builder.RegisterType(typeof (BaseRepository<,>).MakeGenericType(typeContext, t)).As(typeof (IRepository<>).MakeGenericType(t)));
        }
    }
}
