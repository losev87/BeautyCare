using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc.Controls
{
    public class SingleSelectFilterFuncCondition<TEntity> : SingleSelectFilterFuncCondition, IFilterCondition<TEntity>
        where TEntity : class
    {
        private Dictionary<string, Expression<Func<TEntity, bool>>> _expressions;

        public SingleSelectFilterFuncCondition(string column, string caption, IEnumerable<SelectListItem> dictionary, Dictionary<string, Expression<Func<TEntity, bool>>> expressions)
            : base(column, caption, dictionary)
        {
            _expressions = expressions;
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            if (Value != null)
            {
                var value = Value.Values.FirstOrDefault();
                if (value != null)
                {
                    if(_expressions.ContainsKey(value))
                    {
                        return query.Where(_expressions[value]);
                    }
                }
            }
            return query;
        }
    }

    public class SingleSelectFilterFuncCondition : FilterConditionBase
    {
        public IEnumerable<SelectListItem> Dictionary;

        public SingleSelectFilterFuncCondition(string column, string caption, IEnumerable<SelectListItem> dictionary)
            : base(column, caption)
        {
            Dictionary = dictionary;
        }
    }
}