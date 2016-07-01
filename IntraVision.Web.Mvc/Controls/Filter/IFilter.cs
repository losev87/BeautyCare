using System.Collections.Generic;
using System.Linq;
using IntraVision.Web.Mvc.Controls.Filter;

namespace IntraVision.Web.Mvc.Controls
{
    public interface IFilter
    {
        long FilterId { get; }
        bool AllowSave { get; }
        FilterEdit FilterEdit { get; }

        IEnumerable<SavedFilter> SavedFilters { get; }
        IEnumerable<IFilterCondition> Conditions { get; }
    }

    public interface IFilter<TEntity> : IFilter where TEntity : class
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> query);
    }
}
