using System.Collections.Generic;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc
{
    public class EditWithTab
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SelectListItem> Tabs { get; set; }
    }
}
