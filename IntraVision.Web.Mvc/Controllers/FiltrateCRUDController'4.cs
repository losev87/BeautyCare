using IntraVision.Data;
using IntraVision.Web.Mvc.Controls;
using IntraVision.Web.Mvc.Services;

namespace IntraVision.Web.Mvc
{
    public class FiltrateCrudController<TEntity, TEntityGrid, TEntityGridOptions, TEntityFilter> : FiltrateCrudController<TEntity, TEntity, TEntity, TEntityGrid, TEntityGridOptions, TEntityFilter>
        where TEntity : class, IEntityBase
        where TEntityGridOptions : GridOptions
        where TEntityGrid : class, IGridModel<TEntity>
    {
        private IFilterableBaseService<TEntity, TEntity, TEntity, TEntityGrid, TEntityGridOptions, TEntityFilter> _filterableBaseService;
        public FiltrateCrudController(IFilterableBaseService<TEntity, TEntity, TEntity, TEntityGrid, TEntityGridOptions, TEntityFilter> filterableBaseService)
            : base(filterableBaseService)
        {
            _filterableBaseService = filterableBaseService;
        }
    }
}
