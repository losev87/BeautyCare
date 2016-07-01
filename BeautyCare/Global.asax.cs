using BeautyCare.Configuration.Web;

namespace BeautyCare
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebConfiguration.Configure();
        }
    }
}
