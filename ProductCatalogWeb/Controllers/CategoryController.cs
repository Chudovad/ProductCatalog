using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCatalogWeb.Models.Dto;
using ProductCatalogWeb.Services.Interfaces;

namespace ProductCatalogWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            List<CategoryDto> categories = new();

            var response = await _categoryService.GetAllCategoryAsync();

            if (response != null && response.IsSuccess)
            {
                categories = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result));
            }
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryService.CreateCategoryAsync(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["error"] = response.DisplayMessage;
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int categoryId)
        {
            var response = await _categoryService.GetCategoryByIdAsync(categoryId);

            if (response != null && response.IsSuccess)
            {
                CategoryDto model = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));

                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryService.UpdateCategoryAsync(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["error"] = response.DisplayMessage;
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int categoryId)
        {
            var response = await _categoryService.DeleteCategoryAsync(categoryId);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int categoryId)
        {
            CategoryDto model = new CategoryDto();
            var response = await _categoryService.GetCategoryByIdAsync(categoryId);

            if (response != null && response.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));
                ViewData["Title"] = model.Name;
            }
            return View(model);

        }
    }
}
