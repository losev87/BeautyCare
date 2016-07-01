using BeautyCare.Configuration.Autofac;
using BeautyCare.Configuration.Owin;
using Owin;

namespace BeautyCare.Configuration.Web
{
    public static class BootStrapper
    {
        public static void Configure(IAppBuilder app)
        {
            AutofacConfiguration.Configure(app);
            OwinConfiguration.Configure(app);
            //AutoMapperConfiguration.Configure(app);
        }
    }
}
