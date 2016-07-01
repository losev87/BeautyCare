using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IntraVision.Web.Mvc.Controls.Filter
{
    public class FilterEdit
    {
        [TargetProperty("Id"),UIHint("DropdownList"),DisplayName("Сохранить как")]
        public IEnumerable<SelectListItem> Filters { get; set; }
        [ScaffoldColumn(false)]
        public long Id { get; set; }

        [Required,StringLength(255),DisplayName("Название фильтра")]
        public string Name { get; set; }
        public bool Shared { get; set; }
    }
}
