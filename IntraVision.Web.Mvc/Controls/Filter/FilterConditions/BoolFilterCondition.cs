using System;
using System.Linq;
using System.Linq.Dynamic;
using IntraVision.Core;

namespace IntraVision.Web.Mvc.Controls
{
    public class BoolFilterCondition<TEntity> : BoolFilterCondition, IFilterCondition<TEntity>
        where TEntity : class
    {
        public BoolFilterCondition(string column, string caption, string noneText = "", string trueText = "Да", string falseText = "Нет") : base(column, caption, noneText, trueText, falseText) { }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            if (Value == null || string.IsNullOrEmpty(Value.Condition)) return query;

            switch ((Condition)Enum.Parse(typeof(Condition), Value.Condition))
            {
                case Condition.True:
                    return query.Where("{0} == true".AsFormat(Column));
                case Condition.False:
                    return query.Where("{0} == false".AsFormat(Column));
            }

            return query;
        }
    }

    public class BoolFilterCondition : FilterConditionBase
    {
        protected enum Condition
        {
            None,
            True,
            False
        }

        public string NoneText { get; private set; }
        public string TrueText { get; private set; }
        public string FalseText { get; private set; }

        public BoolFilterCondition(string column, string caption, string noneText, string trueText, string falseText)
            : base(column, caption)
        {
            NoneText = noneText;
            TrueText = trueText;
            FalseText = falseText;
        }
    }
}