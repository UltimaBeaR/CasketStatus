using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CasketStatusWebSite.Models
{
    // ViewModel для Login-а юзера (Используется на странице входа)
    public class LoginViewModel
    {
        // Логин
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        // Пароль
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
