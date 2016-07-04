using System.Web.Mvc;
using Autofac.Integration.Mvc;
using BeautyCare.Configuration.Autofac.Modules;
using BeautyCare.Context;
using BeautyCare.Controllers.AZ;
using BeautyCare.Model.Entity;
using BeautyCare.Service;
using BeautyCare.ViewModel.AZ.User;
using IntraVision.Web.Mvc.Autofac;
using IntraVision.Web.Mvc.Autofac.Modules;
using Owin;

namespace BeautyCare.Configuration.Autofac
{
    public static class AutofacConfiguration
    {
        public static void Configure(IAppBuilder app)
        {
            var container = new ContainerAutofacDependencyResolver(
                new ModuleRegisterContext<BeautyCareContext>(),
                new ModuleRegisterBaseRepository<IBeautyCareRepository, BeautyCareContext>(),
                new ModuleRegisterBaseServices(),
                new ModuleRegisterController<AccountController>(),
                new ModuleRegisterGridModel<UserGrid>(),
                new ModuleRegisterServices<IServiceImplementation>()
                ).Container;

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}
