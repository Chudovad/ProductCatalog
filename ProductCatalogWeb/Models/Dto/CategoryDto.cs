using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogWeb.Models.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Название категории обязательное поле")]
        [DisplayName("Название категории")]
        public string Name { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
