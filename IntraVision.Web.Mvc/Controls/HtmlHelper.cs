using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc
{
    public static class HtmlExtension
    {
        public static string RadioWithLabel(this HtmlHelper html, string name, object value, string label, bool selected)
        {
            return string.Format(@"<label><input type=""radio"" name=""{0}"" value=""{1}"" {2} /> {3}</label>", name, value, selected ? " checked=\"checked\"" : "", label);
        }

        public static string CheckboxWithLabel(this HtmlHelper html, string name, object value, string label, bool selected)
        {
            return string.Format(@"<label><input type=""checkbox"" name=""{0}"" value=""{1}"" {2} /> {3}</label>", name, value, selected ? " checked=\"checked\"" : "", label);
        }

        public static string CheckboxWithLabel(this HtmlHelper html, string name, object value, string label, bool selected, Dictionary<string,string> htmlAttribute)
        {
            var attr = string.Join(" ", htmlAttribute.Select(a => string.Format("{0}='{1}'", a.Key, a.Value)));
            return string.Format(@"<label><input type=""checkbox"" name=""{0}"" value=""{1}"" {2} {3} /> {4}</label>", name, value, selected ? " checked=\"checked\"" : "", attr, label);
        }
    }
}
