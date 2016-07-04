using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IntraVision.Web.Mvc.Entities;

namespace BeautyCare.Model.Entity
{
    public class BaseCatalog : EntityBase
    {
        [Display(Name = "Название")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Системное название")]
        [Required]
        [StringLength(100)]
        public string SysName { get; set; }
    }
}
