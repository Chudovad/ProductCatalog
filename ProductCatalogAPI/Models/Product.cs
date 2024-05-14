using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(70, ErrorMessage = "Max length is 70")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [MaxLength(500, ErrorMessage = "Max length is 500")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Price { get; set; }
        [MaxLength(70, ErrorMessage = "Max length is 70")]
        public string GeneralNote { get; set; }
        [MaxLength(70, ErrorMessage = "Max length is 70")]
        public string SpecialNote { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
