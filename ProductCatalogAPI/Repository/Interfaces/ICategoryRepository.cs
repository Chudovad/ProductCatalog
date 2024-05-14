using ProductCatalogAPI.Models.Dto;

namespace ProductCatalogAPI.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<CategoryDto> GetCategoryById(int categoryId);
        Task<CategoryDto> CreateUpdateCategory(CategoryDto categoryDto);
        Task<bool> DeleteCategory(int categoryId);
    }
}
