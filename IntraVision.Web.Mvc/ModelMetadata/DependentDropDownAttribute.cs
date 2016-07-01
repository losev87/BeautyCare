using System;

namespace IntraVision.Web.Mvc
{
    public class DependentDropDownAttribute : Attribute, IAdditionalValueAttribute
    {
        public DependentDropDownAttribute(string parentId, string dataAction)
        {
            ParentId = parentId;
            Action = dataAction;
        }

        public string ParentId { get; set; }
        public string Action { get; set; }
    }
}
