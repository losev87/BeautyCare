using System;
using System.Collections.Generic;

namespace IntraVision.Web.Mvc.Controls
{
    [Serializable]
    public class FilterConditionValue
    {
        public string Column { get; set; }
        public string Condition { get; set; }
        public List<string> Values { get; set; }
    }
}
