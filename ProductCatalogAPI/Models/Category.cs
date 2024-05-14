using System.ComponentModel.DataAnnotations;

namespace ProductCatalogAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(70, ErrorMessage = "Max length is 70")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
