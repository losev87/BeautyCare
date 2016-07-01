using System.Web.Mvc;
using IntraVision.Web.Mvc.Controls;
using IntraVision.Web.Mvc.Controls.Filter;
using IntraVision.Web.Mvc.Security;

namespace IntraVision.Web.Mvc
{
    public interface IFilterableController
    {
        ActionResult Filter(GridOptions options, IMVCPrincipal user);
        JsonResult SaveFilter(FilterEdit filterEdit, GridOptions options, IMVCPrincipal user);
        ActionResult LoadFilter(long id, IMVCPrincipal user);
        ActionResult ClearFilter();
    }
}
