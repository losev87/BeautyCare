using System;
using System.Security.Principal;
using IntraVision.Data;
using IntraVision.Repository;
using IntraVision.Web.Mvc.Controls;

namespace IntraVision.Web.Mvc.Services
{
    public class FilterableBaseService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions, TEntityFilter> : BaseService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions>, IFilterableBaseService<TEntity, TEntityCreate, TEntityEdit, TEntityGrid, TEntityGridOptions, TEntityFilter>
        where TEntity : class, IEntityBase, new()
        where TEntityCreate : class, new()
        where TEntityEdit : class, IEntityBase, new()
        where TEntityGridOptions : GridOptions
        where TEntityFilter : Filter<TEntity>, new()
        where TEntityGrid : class, IGridModel<TEntity>
    {
        public FilterableBaseService(Lazy<IRepository<TEntity>> repository) : base(repository)
        {
        }

        public override ActionGrid<TEntity, TEntityGrid> GetActionGridTEntityList(TEntityGridOptions options, IPrincipal principal)
        {
            var filter = new TEntityFilter().Configure(principal).Init(options);
            var query = filter.Apply(GetEnumerableTEntityView(principal));
            return new ActionGrid<TEntity, TEntityGrid>(query, options).WithFilter(filter).WithUser(principal);
        }

        public virtual Filter<TEntity> GetFilter(TEntityGridOptions options, string gridKey, IPrincipal principal)
        {
            return GetFilterUser(options, gridKey, principal);
        }

        public virtual Filter<TEntity> GetFilterUser(TEntityGridOptions options, string gridKey, IPrincipal principal)
        {
            return new TEntityFilter().Configure(principal).Init(options, gridKey);
        }
    }
}
