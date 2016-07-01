using System.Web.Mvc;

namespace IntraVision.Web.Mvc
{
    public static class InputExtensions
    {
        public static string ValueCheckbox(this HtmlHelper helper, string name, string value, bool selected, string label)
        {
            var cb = string.Format("<input type=\"checkbox\" name=\"{0}\" value=\"{1}\" {2} />", name, value,
                                   selected ? " checked" : "");
            if (string.IsNullOrEmpty(label)) return cb;
            return string.Format("<label>{0} {1}</label>", cb, label);
        }
    }
}
