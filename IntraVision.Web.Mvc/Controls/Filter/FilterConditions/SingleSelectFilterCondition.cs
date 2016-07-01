using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc.Controls
{
    public class SingleSelectFilterCondition<TEntity> : SingleSelectFilterCondition, IFilterCondition<TEntity>
        where TEntity : class
    {
        public SingleSelectFilterCondition(string column, string caption, IEnumerable<SelectListItem> dictionary)
            : base(column, caption, dictionary) { }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            if (Value == null || string.IsNullOrEmpty(Value.Values[0]) || string.IsNullOrEmpty(Value.Condition)) return query;

            var condition = (Condition) Enum.Parse(typeof (Condition), Value.Condition);
            if (condition == Condition.NotEqual) return query.Where(Column + "!=" + Value.Values[0]);
            if (condition == Condition.Equal) return query.Where(Column + "==" + Value.Values[0]);

            return query;
        }
    }

    public class SingleSelectFilterCondition : FilterConditionBase
    {
        protected enum Condition
        {
            Equal,
            NotEqual      
        }

        public IEnumerable<SelectListItem> Dictionary;

        public SingleSelectFilterCondition(string column, string caption, IEnumerable<SelectListItem> dictionary) : base(column, caption) 
        {
            Dictionary = dictionary;
        }
    }
}
