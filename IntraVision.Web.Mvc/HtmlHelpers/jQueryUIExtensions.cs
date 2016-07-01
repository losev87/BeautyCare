using System.Web.Mvc;
using System.Web.Routing;

namespace IntraVision.Web.Mvc
{
    public static class jQueryUIExtensions
    {
        public static string Autocomplete (this HtmlHelper html, string id, string source, object attributes)
        {
            var tagBuilder = new TagBuilder("input");
            var attributesDictionary = new RouteValueDictionary(attributes);
            tagBuilder.Attributes.Add("id", id);
            tagBuilder.Attributes.Add("name", id);
            tagBuilder.Attributes.Add("data-source", source);
            tagBuilder.MergeAttributes(attributesDictionary);
            tagBuilder.AddCssClass("autocomplete");

            return tagBuilder.ToString();
        }
    }
}
