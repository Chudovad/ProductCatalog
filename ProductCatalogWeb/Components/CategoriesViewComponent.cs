using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCatalogWeb.Models.Dto;
using ProductCatalogWeb.Services.Interfaces;

namespace ProductCatalogWeb.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            List<CategoryDto> categories = new();

            var response = _categoryService.GetAllCategoryAsync();

            if (response != null && response.Result.IsSuccess)
            {
                categories = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result.Result));
            }
            return View(categories);
        }
    }
}
