using System.Collections.Generic;
using System.Security.Principal;
using IntraVision.Web.Mvc.Controls;
using IntraVision.Web.Mvc.Controls.Filter;
using IntraVision.Web.Mvc.Security;

namespace IntraVision.Web.Mvc.Services
{
    public interface IFilterService
    {
        IEnumerable<SavedFilter> GetSavedFilters(string filterKey, IPrincipal user);
        void SaveFilter(FilterEdit filterEdit, string filterKey, IGridOptions options, IPrincipal user);
        void DeleteFilter(long id, IMVCPrincipal user);
        GridOptions LoadFilter(long id);
    }
}
