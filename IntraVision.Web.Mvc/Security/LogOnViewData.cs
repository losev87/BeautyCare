using System.ComponentModel.DataAnnotations;

namespace IntraVision.Web.Mvc.Security
{
    public class LogOnViewData : ILogOnViewData
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [StringLength(50), DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}