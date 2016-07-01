using System;
using System.Linq;
using System.Linq.Dynamic;
using IntraVision.Core;

namespace IntraVision.Web.Mvc.Controls
{
    public class NumericFilterCondition<TEntity> : NumericFilterCondition, IFilterCondition<TEntity>
        where TEntity: class
    {
        public NumericFilterCondition(string column, string caption) : base(column, caption) { }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            if (Value == null || string.IsNullOrEmpty(Value.Condition)) return query;

            var condition = ((Condition)Enum.Parse(typeof(Condition), Value.Condition));

            if (Value.Values.Count == 0) return query;
            Decimal value1 = 0;
            Decimal value2 = 0;
            
            switch (condition)
            {
                case Condition.Equal:
                    if (Decimal.TryParse(Value.Values[0], out value1))
                        return query.Where("{0} == @0".AsFormat(Column), value1);
                    else return query;
                case Condition.Between:
                    if (Decimal.TryParse(Value.Values[0], out value1))
                        query = query.Where("{0} >= @0".AsFormat(Column), value1);
                    if (Value.Values.Count >= 2 && Decimal.TryParse(Value.Values[1], out value2))
                        return query.Where("{0} >= @0 && {0} <= @1".AsFormat(Column), value1, value2);
                    else return query;
            }
            return query;
        }
    }

    public class NumericFilterCondition : FilterConditionBase
    {
        protected enum Condition
        {
            Between,
            Equal
        }

        public NumericFilterCondition(string column, string caption) : base(column, caption) { }

        /*
        protected override string GenerateValues(int index)
        {
            var div = new TagBuilder("div");
            div.AddCssClass("filter-value");

            var input0 = new TagBuilder("input");
            input0.MergeAttribute("name", GetInputName(index, "Values[0]"));
            input0.AddCssClass("number");

            var input1 = new TagBuilder("input");
            input1.MergeAttribute("name", GetInputName(index, "Values[1]"));
            input1.AddCssClass("number");

            var condition = Condition.None;
            if (Value != null && !string.IsNullOrEmpty(Value.Condition)) condition = ((Condition)Enum.Parse(typeof(Condition), Value.Condition));

            if (Value != null && Value.Values.Count > 0)
                input0.MergeAttribute("value", Value.Values[0]);
            if (Value != null && condition == Condition.Between && Value.Values.Count > 1)
                input1.MergeAttribute("value", Value.Values[1]);
            else
                input1.MergeAttribute("style", "display:none;");

            div.InnerHtml += input0.ToString();
            div.InnerHtml += input1.ToString();
            return div.ToString();
        }

        protected override IEnumerable<SelectListItem> GetConditions()
        {
            var condition = Value != null && Value.Condition != null ? (Condition)Enum.Parse(typeof(Condition), Value.Condition) : Condition.None;
            return from c in Enum.GetNames(typeof(Condition)) select new SelectListItem { Text = c, Value = c, Selected = c == condition.ToString() };
        }*/
    }
}
