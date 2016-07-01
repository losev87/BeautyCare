using System.Security.Principal;
using IntraVision.Web.Mvc.Controls;

namespace IntraVision.Web.Mvc.Services
{
    public interface IFilterableBaseService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, in TEntityGridOptions, TEntityFilter> : IBaseService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions>
        where TEntity : class
        where TEntityCreate : class
        where TEntityEdit : class
        where TEntityGridOptions : GridOptions
        where TEntityGrid : class, IGridModel<TEntity>
    {
        Filter<TEntity> GetFilter(TEntityGridOptions options, string gridKey, IPrincipal principal);
        Filter<TEntity> GetFilterUser(TEntityGridOptions options, string gridKey, IPrincipal principal);
    }
}
