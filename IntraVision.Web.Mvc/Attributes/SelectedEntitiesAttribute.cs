using System;

namespace IntraVision.Web.Mvc
{
    public class SelectedEntitiesAttribute : Attribute
    {
        public Type Type { get; set; }
    }
}
