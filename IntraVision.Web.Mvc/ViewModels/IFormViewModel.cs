using System.Collections.Generic;

namespace IntraVision.Web.Mvc
{
    public interface IFormViewModel
    {
        IList<string> ExtraScripts { get; }
        IList<string> ExtraStyles { get; }
    }
}
