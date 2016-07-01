using System;

namespace IntraVision.Web.Mvc
{
    public class EditTabAttribute : Attribute
    {
        public string TabName { get; set; }
        public string ActionName { get; set; }
        public int Order { get; set; }
    }
}
