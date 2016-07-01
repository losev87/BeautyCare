using System;
using System.Linq;
using System.Linq.Dynamic;
using IntraVision.Core;

namespace IntraVision.Web.Mvc.Controls
{
    public class StringFilterCondition<TEntity> : StringFilterCondition, IFilterCondition<TEntity>
        where TEntity : class
    {
        public StringFilterCondition(string column, string caption) : base(column, caption) { }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            if (Value == null || string.IsNullOrEmpty(Value.Condition)) return query;

            var condition = (Condition)Enum.Parse(typeof(Condition), Value.Condition);
            if (condition == Condition.None) return query;
            if (condition == Condition.Defined) return query.Where("{0}.Length > 0".AsFormat(Column));
            if (condition == Condition.Undefined) return query.Where("{0} == null || {0}.Length == 0".AsFormat(Column));

            string value = Value.Values[0];
            if(string.IsNullOrEmpty(value)) return query;

            ///TODO Заменить это на Expression?
            switch (condition)
            {
                case Condition.Contains:
                    return query.Where("{0}.Contains(@0)".AsFormat(Column), value);
                case Condition.DoesNotContain:
                    return query.Where("!{0}.Contains(@0)".AsFormat(Column), value);
                case Condition.Equal:
                    return query.Where("{0} == @0".AsFormat(Column), value);
                case Condition.NotEqual:
                    return query.Where("{0} != @0".AsFormat(Column), value);
                case Condition.StartsWith:
                    return query.Where("{0}.StartsWith(@0)".AsFormat(Column), value);
                case Condition.EndsWith:
                    return query.Where("{0}.EndsWith(@0)".AsFormat(Column), value);
            }
            return query;
        }
    }

    public class StringFilterCondition : FilterConditionBase
    {
        protected enum Condition
        {
            None,
            Contains,
            DoesNotContain,
            Equal,
            NotEqual,
            StartsWith,
            EndsWith,
            Defined,
            Undefined
        }

        public StringFilterCondition(string column,string caption) : base(column, caption) { }
     }
}
