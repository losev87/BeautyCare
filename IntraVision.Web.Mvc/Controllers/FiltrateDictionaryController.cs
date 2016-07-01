using IntraVision.Data;
using IntraVision.Web.Mvc.Services;

namespace IntraVision.Web.Mvc
{
    public class FiltrateDictionaryController<TEntity, TEntityCreate, TEntityEdit> : FiltrateCrudController<TEntity, TEntityCreate, TEntityEdit, GridModelBase<TEntity>, GridOptionsBase<TEntity>, FilterBase<TEntity>>
        where TEntity : class, IEntityBase, INamedEntityBase
        where TEntityCreate : class
        where TEntityEdit : class
    {
        public FiltrateDictionaryController(IFilterableBaseService<TEntity, TEntityCreate, TEntityEdit, GridModelBase<TEntity>, GridOptionsBase<TEntity>, FilterBase<TEntity>> filterableBaseService)
            : base(filterableBaseService)
        {
        }
    }
}
