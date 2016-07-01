using System;

namespace IntraVision.Web.Mvc
{
    public class GridColumnAttribute : Attribute
    {
        public string Property { get; set; }
        public string Sortable { get; set; }
        public int Order { get; set; }
        public string Named { get; set; }
        public bool EditLink { get; set; }
        public string ActionEditLink { get; set; }
        public bool DoNotEncode { get; set; }
        public string Css { get; set; }
    }
}
