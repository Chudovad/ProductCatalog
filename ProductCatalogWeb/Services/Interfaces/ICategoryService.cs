using ProductCatalogWeb.Models.Dto;

namespace ProductCatalogWeb.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponseDto> GetAllCategoryAsync();
        Task<ResponseDto> GetCategoryByIdAsync(int id);
        Task<ResponseDto> CreateCategoryAsync(CategoryDto categoryDto);
        Task<ResponseDto> UpdateCategoryAsync(CategoryDto categoryDto);
        Task<ResponseDto> DeleteCategoryAsync(int id);
    }
}
