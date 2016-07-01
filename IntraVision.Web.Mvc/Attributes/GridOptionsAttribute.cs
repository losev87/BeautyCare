using System;
using IntraVision.Core.Sorting;

namespace IntraVision.Web.Mvc
{
    public class GridOptionsAttribute : Attribute
    {
        public string Column { get; set; }
        public SortDirection Direction { get; set; }
        public int Order { get; set; }
    }
}
