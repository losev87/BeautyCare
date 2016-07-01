using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IntraVision.Mvc.DataAnnotations
{
    public class ValidationRunner
    {
        public static List<string> ValidateObject<T>(T obj)
        {
            var type = typeof(T);
            var meta = type.GetCustomAttributes(false).OfType<MetadataTypeAttribute>().FirstOrDefault();
            if (meta != null)
            {
                type = meta.MetadataClassType;
            }
            var propertyInfo = type.GetProperties();
            return (from info in propertyInfo
                    let attributes = info.GetCustomAttributes(false).OfType<ValidationAttribute>()
                    from attribute in attributes
                    let objPropInfo = obj.GetType().GetProperty(info.Name)
                    where !attribute.IsValid(objPropInfo.GetValue(obj, null))
                    select attribute.FormatErrorMessage(info.Name)).ToList();
        }
    }
}
