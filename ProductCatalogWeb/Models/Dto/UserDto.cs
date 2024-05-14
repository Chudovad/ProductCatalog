using System.ComponentModel.DataAnnotations;

namespace ProductCatalogWeb.Models.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Email обязательное поле")]
        public string Email { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Роль")]
        public string Role { get; set; }
    }
}
