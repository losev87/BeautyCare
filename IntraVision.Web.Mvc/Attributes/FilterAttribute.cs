using System;

namespace IntraVision.Web.Mvc
{
    public class FilterGridAttribute : Attribute
    {
        public string Column { get; set; }
        public int Order { get; set; }
        public string Named { get; set; }
        public Type Type { get; set; }
    }
}
