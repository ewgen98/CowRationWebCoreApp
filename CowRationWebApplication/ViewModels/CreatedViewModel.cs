using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CowRationWebApplication.ViewModels
{
    public class CreatedViewModel
    {
        [Display(Name ="Название организации")]
        [Required(ErrorMessage ="Введите название организации")]
        public string OrganizationName { get; set; }

        [Display(Name ="E-mail адресс")]
        [Required(ErrorMessage ="Введите e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="Пароль")]
        [Required(ErrorMessage ="Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Повторите пароль")]
        [Required(ErrorMessage ="Повторите пароль")]
        [Compare("Password", ErrorMessage ="Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
