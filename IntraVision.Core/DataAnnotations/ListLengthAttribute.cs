using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace IntraVision.DataAnnotations
{
    public class ListRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var col = value as ICollection;
            return !(col == null || col.Count == 0);
        }
    }
}
