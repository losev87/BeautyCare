using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using IntraVision.Web.Mvc.Attributes;

namespace BeautyCare.ViewModel.AZ.User
{
    public class UserCreate
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Логин*")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [StringLength(256)]
        public string Login { get; set; }

        [Display(Name = "Email*")]
        [Email(ErrorMessage = "Адрес введен некорректно")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [StringLength(256)]
        public string Email { get; set; }

        [Display(Name = "Пароль*")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля*")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string ConfirmPassword { get; set; }

        public override string ToString()
        {
            return "Пользователь";
        }
    }
}
