using ProductCatalogAPI.Models.Dto;

namespace ProductCatalogAPI.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int productId);
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(int productId);
        Task<IEnumerable<ProductDto>> SearchProduct(string searchText);
        Task<IEnumerable<ProductDto>> FilterProductByPrice(int priceStart, int priceEnd);
    }
}
