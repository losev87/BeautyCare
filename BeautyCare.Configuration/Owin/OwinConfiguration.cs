using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace BeautyCare.Configuration.Owin
{
    public static class OwinConfiguration
    {
        public static void Configure(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/AZ/Account/Login"),
                Provider = new CookieAuthenticationProvider()
            });
        }
    }
}
