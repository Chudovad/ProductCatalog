using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Models;
using ProductCatalogAPI.Models.Dto;
using ProductCatalogAPI.Repository.Interfaces;

namespace ProductCatalogAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public CategoryRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateUpdateCategory(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<CategoryDto, Category>(categoryDto);

            if (category.Id > 0)
            {
                _context.Categories.Update(category);
            }
            else
            {
                if (!await _context.Categories.AnyAsync(i => i.Name.Equals(category.Name)))
                    await _context.Categories.AddAsync(category);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<Category, CategoryDto>(category);
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            try
            {
                Category category = _context.Categories.FirstOrDefault(x => x.Id == categoryId);
                if (category != null)
                {
                    _context.Products.RemoveRange(_context.Products.Where(u => u.Category == category));
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            List<Category> categoryList = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryDto>>(categoryList);
        }

        public async Task<CategoryDto> GetCategoryById(int categoryId)
        {
            Category category = await _context.Categories.Include(u => u.Products).FirstOrDefaultAsync(i => i.Id == categoryId);
            return _mapper.Map<CategoryDto>(category);
        }
    }
}
