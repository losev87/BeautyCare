using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc.Controls
{
    public class SelectFilterFuncCondition<TEntity> : SelectFilterCondition, IFilterCondition<TEntity>
        where TEntity : class
    {
        private Func<IQueryable<TEntity>, List<string>, IQueryable<TEntity>> _func;

        public SelectFilterFuncCondition(string column, string caption, IEnumerable<SelectListItem> dictionary, Func<IQueryable<TEntity>, List<string>, IQueryable<TEntity>> func)
            : base(column, caption, dictionary)
        {
            _func = func;
        }

        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            if (Value == null || string.IsNullOrEmpty(Value.Condition)) return query;

            return _func.Invoke(query, Value.Values);
        }
    }
}
