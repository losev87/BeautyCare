using System.Collections.Generic;

namespace IntraVision.Web.Mvc
{
    public class FormViewModelBase : IFormViewModel
    {
        protected List<string> _ExtraScripts = new List<string>();
        protected List<string> _ExtraStyles = new List<string>();

        public virtual IList<string> ExtraScripts { get { return _ExtraScripts; } }
        public virtual IList<string> ExtraStyles { get { return _ExtraStyles; } }
    }
}
