using System.ComponentModel.DataAnnotations;

namespace IntraVision.Web.Mvc.Validation
{
    public class AutocompleteRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var acf = value as AutocompleteField;
            return (acf != null && acf.Id > 0);
        }
    }
}
