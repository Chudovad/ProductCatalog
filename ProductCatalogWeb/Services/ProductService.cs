using ProductCatalogWeb.Models.Dto;
using ProductCatalogWeb.Services.Interfaces;

namespace ProductCatalogWeb.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> CreateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.POST,
                Data = productDto,
                Url = StaticData.ProductCatalogAPIBase + "/api/products"
            });
        }

        public async Task<ResponseDto> DeleteProductAsync(int id)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.DELETE,
                Url = StaticData.ProductCatalogAPIBase + "/api/products/" + id
            });
        }

        public async Task<ResponseDto> FilterProductByPriceAsync(int priceStart, int priceEnd)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.ProductCatalogAPIBase + $"/api/products/filter?priceStart={priceStart}&priceEnd={priceEnd}"
            }, withBearer: false);
        }

        public async Task<ResponseDto> GetAllProductAsync()
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.ProductCatalogAPIBase + "/api/products"
            }, withBearer: false);
        }

        public async Task<ResponseDto> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.ProductCatalogAPIBase + "/api/products/" + id
            }, withBearer: false);
        }

        public async Task<ResponseDto> SearchProductAsync(string searchText)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.ProductCatalogAPIBase + "/api/products/search?searchText=" + searchText
            }, withBearer: false);
        }

        public async Task<ResponseDto> UpdateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.PUT,
                Data = productDto,
                Url = StaticData.ProductCatalogAPIBase + "/api/products"
            });
        }
    }
}
