using System.ComponentModel.DataAnnotations;

namespace IntraVision.Web.Mvc.Security
{
    public class CodeViewData 
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Код")]
        public string Code { get; set; }
    }
}