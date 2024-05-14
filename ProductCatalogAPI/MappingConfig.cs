using AutoMapper;
using ProductCatalogAPI.Models;
using ProductCatalogAPI.Models.Dto;

namespace ProductCatalogAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
                config.CreateMap<CategoryDto, Category>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
