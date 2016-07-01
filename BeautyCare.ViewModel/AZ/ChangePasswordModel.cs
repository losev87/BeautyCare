using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BeautyCare.ViewModel.AZ
{
    public class ChangePasswordModel
    {
        [HiddenInput]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Укажите текущий пароль.")]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Укажите новый пароль.")]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Пароли не совпадают.")]
        [Display(Name = "Повторите новый пароль")]
        public string ConfirmPassword { get; set; }
    }
}
