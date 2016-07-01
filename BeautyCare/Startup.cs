using BeautyCare.Configuration.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeautyCare.Startup))]
namespace BeautyCare
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            BootStrapper.Configure(app);
        }
    }
}
