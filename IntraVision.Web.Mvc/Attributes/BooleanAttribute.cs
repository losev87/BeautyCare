using System.ComponentModel.DataAnnotations;

namespace IntraVision.Web.Mvc
{
    public class BooleanAttribute : ValidationAttribute
    {
        public bool Value { get; set; }

        public override bool IsValid(object value)
        {
            return value != null && value is bool && (bool)value == Value;
        }
    }
}
