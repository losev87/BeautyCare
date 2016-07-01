using System;
using System.Web;
using IntraVision.Web.Mvc.Autofac;

namespace IntraVision.Web.Mvc.Security
{
    public class AuthenticationModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += (new EventHandler(this.Application_AuthenticateRequest));
        }

        private void Application_AuthenticateRequest(Object source, EventArgs e)
        {
            var application = (HttpApplication)source;
            var context = application.Context;

            var persister = GetInLifetimeScope.Instance<IUserPersister>();
            context.User = persister.GetPrincipal();
        }

        public void Dispose()
        {
        }
    }
}