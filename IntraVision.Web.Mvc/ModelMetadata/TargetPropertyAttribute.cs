using System;

namespace IntraVision.Web.Mvc
{
    public class TargetPropertyAttribute : Attribute, IAdditionalValueAttribute
    {
        public TargetPropertyAttribute(string targetProperty)
        {
            TargetProperty = targetProperty;
        }

        public string TargetProperty { get; set; }
    }
}
