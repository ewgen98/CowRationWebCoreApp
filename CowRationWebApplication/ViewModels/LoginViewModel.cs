using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CowRationWebApplication.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Пользователя с таким именем не существует")]
        public string EmailOrLogin { get; set; }

        [Required(ErrorMessage ="Введен не верный пароль")]
        public string Password { get; set; }
    }
}
