using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc.Controls
{
    public class SelectFilterCondition<TEntity> : SelectFilterCondition, IFilterCondition<TEntity>
        where TEntity : class
    {
        private Func<TEntity, object> _expression;
        public SelectFilterCondition(string column, string caption, IEnumerable<SelectListItem> dictionary)
            : base(column, caption, dictionary) { }

        public SelectFilterCondition(Func<TEntity, object> expression, string caption,
            IEnumerable<SelectListItem> dictionary)
            : base("", caption, dictionary)
        {
            _expression = expression;
        }

        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            if (Value == null || string.IsNullOrEmpty(Value.Condition)) return query;

            var condition = (Condition)Enum.Parse(typeof(Condition), Value.Condition);
            if (condition == Condition.Defined) return query.Where(Column + "!=null");
            if (condition == Condition.Undefined) return query.Where(Column + "==null");

            var ids = Value.Values;
            ///TODO Заменить это на Expression?
            if (ids != null && ids.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                string op = (condition == Condition.IsIn) ? "==" : "!=";
                string dv = (condition == Condition.IsIn) ? "||" : "&&";
                foreach (string id in ids)
                {
                    if (string.IsNullOrEmpty(id)) continue;
                    if (sb.Length > 0) sb.Append(dv);
                    sb.Append(Column).Append(op).Append(id);
                }
                if (sb.Length > 0 )
                    return query.Where(sb.ToString());
            }
            return query;
        }
    }

    public class SelectFilterCondition : FilterConditionBase
    {
        protected enum Condition
        {
            IsIn,
            IsNotIn,
            Defined,
            Undefined
        }

        public IEnumerable<SelectListItem> Dictionary;

        public SelectFilterCondition(string column, string caption, IEnumerable<SelectListItem> dictionary) : base(column, caption) 
        {
            Dictionary = dictionary;
        }
    }
}
