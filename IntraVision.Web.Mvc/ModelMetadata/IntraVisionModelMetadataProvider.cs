using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel;

namespace IntraVision.Web.Mvc
{
    public class IntraVisionModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            var additionalValueAttributes = attributes.Where(a => typeof(IAdditionalValueAttribute).IsAssignableFrom(a.GetType()));

            foreach (var attr in additionalValueAttributes)
                metadata.AdditionalValues.Add(attr.GetType().Name, attr);

            //Ugly workaround for bug with description attribute
            //About: http://stackoverflow.com/questions/2826083/how-to-set-the-description-property-of-the-modelmetadata
            //TOFIX 
            var descriptionAttribute = attributes.SingleOrDefault(a => typeof(DescriptionAttribute).IsAssignableFrom(a.GetType()));
            if (descriptionAttribute != null)
                metadata.Description = (descriptionAttribute as DescriptionAttribute).Description;

            return metadata;
        }
    }
}
