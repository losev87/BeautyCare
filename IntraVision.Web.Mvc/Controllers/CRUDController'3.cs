using IntraVision.Data;
using IntraVision.Web.Mvc.Controls;
using IntraVision.Web.Mvc.Services;

namespace IntraVision.Web.Mvc
{
    public class CrudController<TEntity, TEntityGrid, TEntityGridOptions> : CrudController<TEntity, TEntity, TEntity, TEntityGrid, TEntityGridOptions>
        where TEntity : class, IEntityBase
        where TEntityGridOptions : GridOptions
        where TEntityGrid : class, IGridModel<TEntity>
    {
        public CrudController(IBaseService<TEntity, TEntity, TEntity, TEntityGrid, TEntityGridOptions> service)
            :base(service)
        {
        }
    }
}
