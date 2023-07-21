using System.ComponentModel.DataAnnotations;

namespace WebAppCoreV3.Models {

    public class LoginViewModel {

        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required]
        [UIHint("password")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}
