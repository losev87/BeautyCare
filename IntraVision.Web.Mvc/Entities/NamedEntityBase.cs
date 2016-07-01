using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using IntraVision.Core.Sorting;
using IntraVision.Data;

namespace IntraVision.Web.Mvc.Entities
{
    public class NamedEntityBase : INamedEntityBase
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название", Order = 1)]
        [StringLength(100, ErrorMessage = "Название не должно быть более 100 символов")]
        [GridColumn(ActionEditLink = "Edit", Css = "dialog-form", Property = "Name")]
        [GridOptions(Column = "Name", Direction = SortDirection.Ascending)]
        public string Name { get; set; }
    }
}
