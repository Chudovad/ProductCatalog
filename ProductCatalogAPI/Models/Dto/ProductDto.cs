namespace ProductCatalogAPI.Models.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string GeneralNote { get; set; }
        public string SpecialNote { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
