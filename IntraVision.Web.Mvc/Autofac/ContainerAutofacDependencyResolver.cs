using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace IntraVision.Web.Mvc.Autofac
{
    public class ContainerAutofacDependencyResolver
    {
        public IContainer Container { get { return _containerBuilder.Build(); } }

        private ContainerBuilder _containerBuilder;

        public ContainerAutofacDependencyResolver(params Module[] modules)
        {
            _containerBuilder = new ContainerBuilder();
            _containerBuilder.RegisterModule(new AutofacWebTypesModule());//Инициализация классов HttpContextBase, HttpRequestBase, HttpResponseBase, HttpServerUtilityBase, HttpSessionStateBase, HttpApplicationStateBase, HttpBrowserCapabilitiesBase, HttpCachePolicyBase, VirtualPathProvider
            _containerBuilder.RegisterSource(new ViewRegistrationSource());//Инжекция во View

            foreach (var module in modules)
            {
                _containerBuilder.RegisterModule(module);
            }
        }

        public ContainerAutofacDependencyResolver RegisterFilterProvider()
        {
            _containerBuilder.RegisterFilterProvider();
            return this;
        }

        public ContainerAutofacDependencyResolver RegisterWebApiFilterProvider(HttpConfiguration httpConfiguration)
        {
            _containerBuilder.RegisterWebApiFilterProvider(httpConfiguration);
            return this;
        }
    }
}
