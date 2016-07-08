using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using BeautyCare.Model.Entity;
using BeautyCare.Model.Management;
using IntraVision.Web.Mvc;
using IntraVision.Web.Mvc.Attributes;
using IntraVision.Web.Mvc.Autofac;

namespace BeautyCare.ViewModel.AZ
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

        [Display(Name = "Phone*")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [StringLength(256)]
        public string Phone { get; set; }

        [Display(Name = "Пароль*")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля*")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Отчество")]
        public string PatronimicName { get; set; }
        [ScaffoldColumn(false)]
        public int? GenderId { get; set; }
        [Display(Name = "Обращение")]
        [UIHint("DropDownList")]
        [TargetProperty("GenderId")]
        public IEnumerable<SelectListItem> BrandDictionary
        {
            get
            {
                var repository = GetInLifetimeScope.Repository<Gender>();

                if (repository != null)
                {
                    var dictionary = new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
                    dictionary.AddRange(repository.GetQuery().OrderBy(d => d.Name).ToSelectList(c => c.Id, c => c.Name, c => c.Id == GenderId));

                    return dictionary;
                }
                else
                {
                    return new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
                }
            }
        }
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }
        //public virtual UserType UserType { get; set; }
        //public int UserTypeId { get; set; }


        public override string ToString()
        {
            return "Пользователь";
        }
    }
}
