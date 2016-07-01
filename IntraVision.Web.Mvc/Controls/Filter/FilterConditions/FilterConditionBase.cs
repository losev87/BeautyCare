using System.Collections.Generic;

namespace IntraVision.Web.Mvc.Controls
{
    public class FilterConditionBase : IFilterCondition
    {
        protected List<string> _ConditionNames;
        public FilterConditionValue Value { get; set; }

        public string Column { get; private set; }
        public string Caption { get; private set; }

        protected FilterConditionBase(string column, string caption)
        {
            Column = column;
            Caption = caption;
            _ConditionNames = new List<string>();
        }

        public virtual string GetInputName(int index, string field)
        {
            return string.Format("FilterConditions[{0}].{1}", index, field);
        }

        public virtual string GetInputId(int index, string field)
        {
            return string.Format("FilterConditions{0}_{1}", index, field);
        }
    }
}
