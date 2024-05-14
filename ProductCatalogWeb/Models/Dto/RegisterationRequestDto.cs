using System.ComponentModel.DataAnnotations;

namespace ProductCatalogWeb.Models.Dto
{
    public class RegistrationRequestDto
    {
        [Required(ErrorMessage = "Email обязательное поле")]
        public string Email { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль обязательное поле")]
        public string Password { get; set; }
        [Display(Name = "Роль")]
        [Required(ErrorMessage = "Роль обязательное поле")]
        public string Role { get; set; }
    }
}
