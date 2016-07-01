using IntraVision.Data;
using IntraVision.Web.Mvc.Services;

namespace IntraVision.Web.Mvc
{
    public class DictionaryController<TEntity, TEntityCreate, TEntityEdit> : CrudController<TEntity, TEntityCreate, TEntityEdit, GridModelBase<TEntity>, GridOptionsBase<TEntity>>
        where TEntity : class, INamedEntityBase
        where TEntityCreate : class
        where TEntityEdit : class
    {
        public DictionaryController(IBaseService<TEntity, TEntityCreate, TEntityEdit, GridModelBase<TEntity>, GridOptionsBase<TEntity>> baseService)
            :base(baseService)
        {
        }
    }
}
