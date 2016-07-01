using System.Security.Principal;
using IntraVision.Web.Mvc.Controls;

namespace IntraVision.Web.Mvc.Services
{
    public interface IDependentEntityService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions> 
        : IBaseService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions>
        where TEntity : class
        where TEntityCreate : class
        where TEntityEdit : class
        where TEntityGrid : class, IGridModel<TEntity>
        where TEntityGridOptions : GridOptions
    {
        TEntityCreate CreateDependent(int referevceId);
        ActionGrid<TEntity, TEntityGrid> GridByReferenceId(TEntityGridOptions options, IPrincipal principal, int referevceId);
    }
}
