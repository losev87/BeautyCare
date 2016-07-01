using System;
using System.Linq;
using System.Linq.Dynamic;
using IntraVision.Core;

namespace IntraVision.Web.Mvc.Controls
{
    public class DateFilterCondition<TEntity> : DateFilterCondition, IFilterCondition<TEntity>
        where TEntity : class
    {
        public DateFilterCondition(string column, string caption) : base(column, caption) { }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            if (Value == null || string.IsNullOrEmpty(Value.Condition)) return query;

            var condition = ((Condition)Enum.Parse(typeof(Condition), Value.Condition));

            if (Value.Values.Count == 0) return query;
            var value1 = DateTime.MinValue;
            var value2 = DateTime.MinValue;

            switch (condition)
            {
                case Condition.Equal:
                    if (!DateTime.TryParse(Value.Values[0], out value1)) return query;
                    bool d1t = value1.Hour > 0 || value1.Minute > 0;
                    if (d1t) return query.Where("{0} == @0".AsFormat(Column), value1);

                    //Ugly workaround for a Linq2NH bug with dates: http://stackoverflow.com/questions/1724239/compare-only-date-in-nhibernate-linq-on-a-datetime-value
                    var startDate = new DateTime(value1.Date.Year, value1.Date.Month, value1.Date.Day, 0, 0, 0);
                    var endDate = new DateTime(value1.Date.Year, value1.Date.Month, value1.Date.Day, 23, 59, 59);
                    return query.Where("{0} >= @0 && {0} <= @1".AsFormat(Column), startDate, endDate);
                case Condition.Between:
                    if (DateTime.TryParse(Value.Values[0], out value1))
                    {
                        query = query.Where("{0} >= @0".AsFormat(Column), value1);
                    }

                    if (Value.Values.Count >= 2 && DateTime.TryParse(Value.Values[1], out value2))
                    {
                        bool d2t = value2.Hour > 0 || value2.Minute > 0;
                        var endDate2 = d2t ? value2 : new DateTime(value2.Year, value2.Month, value2.Day, 23, 59, 59);
                        return query.Where("{0} <= @0".AsFormat(Column), endDate2);
                    }
                    else return query;
            }
            return query;
        }
    }

    public class DateFilterCondition : FilterConditionBase
    {
        protected enum Condition
        {
            Equal,
            Between,
        }

        public DateFilterCondition(string column, string caption) : base(column, caption) { }

        /*
        protected override string GenerateValues(int index)
        {
            var div = new TagBuilder("div");
            div.AddCssClass("filter-value");

            var input0 = new TagBuilder("input");
            input0.MergeAttribute("name", GetInputName(index, "Values[0]"));
            input0.AddCssClass("dateRU");
            var input1 = new TagBuilder("input");
            input1.MergeAttribute("name", GetInputName(index, "Values[1]"));
            input1.AddCssClass("dateRU");

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
            var condition = Value != null && !string.IsNullOrEmpty(Value.Condition) ? (Condition)Enum.Parse(typeof(Condition), Value.Condition) : Condition.None;
            return from c in Enum.GetNames(typeof(Condition)) select new SelectListItem { Text = c, Value = c, Selected = c == condition.ToString() };
        }*/
    }
}
