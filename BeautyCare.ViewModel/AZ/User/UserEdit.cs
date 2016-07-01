using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using IntraVision.Web.Mvc.Attributes;

namespace BeautyCare.ViewModel.AZ.User
{
    public class UserEdit
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "\"{0}\" required field")]
        [StringLength(256)]
        public string Login { get; set; }

        [Display(Name = "Email")]
        [Email(ErrorMessage = "Адрес введен некорректно")]
        [Required(ErrorMessage = "\"{0}\" required field")]
        [StringLength(256)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        //[Required(ErrorMessage = "\"{0}\" required field")]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        //[Required(ErrorMessage = "\"{0}\" required field")]
        public string ConfirmPassword { get; set; }

        public override string ToString()
        {
            return "Пользователь";
        }
    }
}