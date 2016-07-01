using System;

namespace IntraVision.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ImageAttribute : Attribute
    {
        public int? Width { get; set; }
        public string PropertyName { get; set; }
    }
}
