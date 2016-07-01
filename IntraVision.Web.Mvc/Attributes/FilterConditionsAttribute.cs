using System;
using System.Collections.Generic;
using System.Linq;

namespace IntraVision.Web.Mvc
{
    public class FilterConditionsAttribute : Attribute
    {
        public FilterConditionsAttribute(params string[] values)
        {
            Values = values.ToList();
        }

        public string Column { get; set; }
        public string Condition { get; set; }
        public List<string> Values { get; set; }
    }
}
