using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Models;
using ProductCatalogAPI.Models.Dto;
using ProductCatalogAPI.Repository.Interfaces;

namespace ProductCatalogAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto, Product>(productDto);

            if (product.Id > 0)
            {
                _context.Products.Update(product);
            }
            else
            {
                await _context.Products.AddAsync(product);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product product = _context.Products.FirstOrDefault(x => x.Id == productId);
                if (product != null)
                {
                    _context.Products.Remove(product);
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

        public async Task<IEnumerable<ProductDto>> FilterProductByPrice(int priceStart, int priceEnd)
        {
            List<Product> products = await _context.Products.Include(i => i.Category).Where(i => i.Price >= priceStart && i.Price <= priceEnd).ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(i => i.Id == productId);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> productList = await _context.Products.Include(u => u.Category).ToListAsync();
            return _mapper.Map<List<ProductDto>>(productList);
        }

        public async Task<IEnumerable<ProductDto>> SearchProduct(string searchText)
        {
            List<Product> products = await _context.Products.Include(i => i.Category)
                .Where(i =>
                    i.Name.Contains(searchText) ||
                    i.Description.Contains(searchText) ||
                    i.GeneralNote.Contains(searchText))
                .ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
