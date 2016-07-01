using System.Collections.Generic;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc
{
    public abstract class ExtendedWebViewPage<T> : WebViewPage<T>
    {
        public IList<string> CustomScripts { get; set; }
        public IList<string> CustomCss { get; set; }

        public ExtendedWebViewPage()
        {
            CustomScripts = new List<string>();
            CustomCss = new List<string>();
        }
    }
}
