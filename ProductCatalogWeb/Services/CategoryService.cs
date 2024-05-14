using ProductCatalogWeb.Models.Dto;
using ProductCatalogWeb.Services.Interfaces;

namespace ProductCatalogWeb.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseService _baseService;
        public CategoryService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> CreateCategoryAsync(CategoryDto categoryDto)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.POST,
                Data = categoryDto,
                Url = StaticData.ProductCatalogAPIBase + "/api/categories"
            });
        }

        public async Task<ResponseDto> DeleteCategoryAsync(int id)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.DELETE,
                Url = StaticData.ProductCatalogAPIBase + "/api/categories/" + id
            });
        }

        public async Task<ResponseDto> GetAllCategoryAsync()
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.ProductCatalogAPIBase + "/api/categories"
            });
        }

        public async Task<ResponseDto> GetCategoryByIdAsync(int id)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.ProductCatalogAPIBase + "/api/categories/" + id
            });
        }

        public async Task<ResponseDto> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.PUT,
                Data = categoryDto,
                Url = StaticData.ProductCatalogAPIBase + "/api/categories"
            });
        }
    }
}
