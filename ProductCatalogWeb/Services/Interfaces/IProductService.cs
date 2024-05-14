using ProductCatalogWeb.Models.Dto;

namespace ProductCatalogWeb.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResponseDto> GetAllProductAsync();
        Task<ResponseDto> GetProductByIdAsync(int id);
        Task<ResponseDto> CreateProductAsync(ProductDto productDto);
        Task<ResponseDto> UpdateProductAsync(ProductDto productDto);
        Task<ResponseDto> DeleteProductAsync(int id);
        Task<ResponseDto> SearchProductAsync(string searchText);
        Task<ResponseDto> FilterProductByPriceAsync(int priceStart, int priceEnd);
    }
}
