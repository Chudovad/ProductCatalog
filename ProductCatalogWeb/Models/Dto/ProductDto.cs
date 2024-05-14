using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProductCatalogWeb.Models.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Наименование продукта обязательное поле")]
        [DisplayName("Наименование продукта")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Описание обязательное поле")]
        [DisplayName("Описание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Стоимость в рублях обязательное поле")]
        [DisplayName("Стоимость в рублях")]
        [Range(0.01, 10000.00, ErrorMessage = "Цена должна быть в пределах от 0,01 до 10000,00")]
        public decimal Price { get; set; }
        [DisplayName("Примечание общее")]
        public string GeneralNote { get; set; }
        [DisplayName("Примечание специальное")]
        public string SpecialNote { get; set; }
        [DisplayName("Категория")]
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
