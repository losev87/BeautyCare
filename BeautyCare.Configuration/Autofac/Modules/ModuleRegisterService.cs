using System.Linq;
using System.Web;
using Autofac;
using BeautyCare.Context;
using BeautyCare.Model.Management;
using BeautyCare.Service;
using IntraVision.Core;
using IntraVision.Data;
using IntraVision.Web.Mvc.Services;
using Microsoft.Owin.Security;

namespace BeautyCare.Configuration.Autofac.Modules
{
    public class ModuleRegisterServices<TServiceImplementationInterface> : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserStoreBase<BeautyCareContext, User, Role, IdentityUserLoginBase, UserRole, IdentityUserClaimBase>>().AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterType<RoleStoreBase<BeautyCareContext, User, Role, IdentityUserLoginBase, UserRole, IdentityUserClaimBase>>().AsImplementedInterfaces().InstancePerRequest();

            builder.Register(componentContext => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>().InstancePerRequest();
            builder.RegisterType<UserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<RoleManagerBase<User, Role, IdentityUserLoginBase, UserRole, IdentityUserClaimBase>>().AsSelf().InstancePerRequest();
            
            typeof(TServiceImplementationInterface).Assembly.GetTypes()
                .Where(t => typeof(TServiceImplementationInterface).IsAssignableFrom(t))
                .ForEach(t => builder.RegisterType(t).AsImplementedInterfaces());

        }
    }
}
