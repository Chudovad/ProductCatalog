using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProductCatalogWeb.Models;
using ProductCatalogWeb.Models.Dto;
using ProductCatalogWeb.Services.Interfaces;
using System.Diagnostics;

namespace ProductCatalogWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new();

            var response = await _productService.GetAllProductAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string searchText, int priceStart, int priceEnd)
        {
            List<ProductDto> list = new();
            ResponseDto response = new();

            if (!string.IsNullOrEmpty(searchText))
            {
                response = await _productService.SearchProductAsync(searchText);
                ViewData["searchText"] = searchText;
            }
            else
            {
                response = await _productService.FilterProductByPriceAsync(priceStart, priceEnd);
                ViewData["priceStart"] = priceStart;
                ViewData["priceEnd"] = priceEnd;
            }

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryList = await GetSelectListsCategory();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["error"] = response.DisplayMessage;
            }
            ViewBag.CategoryList = await GetSelectListsCategory();
            return View(model);
        }

        public async Task<IActionResult> Edit(int productId)
        {
            var response = await _productService.GetProductByIdAsync(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                ViewBag.CategoryList = await GetSelectListsCategory();
                return View(model);
            }
            return NotFound();
        }

        private async Task<IEnumerable<SelectListItem>> GetSelectListsCategory()
        {
            var response = await _categoryService.GetAllCategoryAsync();

            if (response != null && response.IsSuccess)
            {
                var categoryList = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result));
                var categorySelectList = categoryList.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
                return categorySelectList;
            }
            return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["error"] = response.DisplayMessage;
            }
            ViewBag.CategoryList = await GetSelectListsCategory();
            return View(model);
        }

        public async Task<IActionResult> Delete(int productId)
        {
            var response = await _productService.DeleteProductAsync(productId);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
