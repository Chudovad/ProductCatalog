using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductCatalogWeb.Models.Dto;
using ProductCatalogWeb.Services.Interfaces;

namespace ProductCatalogWeb.Controllers
{
    [Authorize(Roles = StaticData.RoleAdmin)]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            List<UserDto> usersDto = new();
            var users = await _adminService.GetAllUserAsync();

            if (users != null)
            {
                usersDto = JsonConvert.DeserializeObject<List<UserDto>>(Convert.ToString(users.Result));
            }
            return View(usersDto);
        }

        public IActionResult Create()
        {
            ViewBag.RoleList = AuthController.SelectListRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistrationRequestDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _adminService.CreateUserAsync(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["error"] = response.DisplayMessage;
            }
            ViewBag.RoleList = AuthController.SelectListRoles();
            return View(model);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var response = await _adminService.GetUserByIdAsync(userId);

            if (response != null && response.IsSuccess)
            {
                UserDto model = JsonConvert.DeserializeObject<UserDto>(Convert.ToString(response.Result));

                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] string Id, [FromForm] string Password)
        {
            if (ModelState.IsValid)
            {
                var response = await _adminService.ChangeUserPasswordAsync(Id, Password);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["error"] = response.DisplayMessage;
            }

            return View();
        }

        public async Task<IActionResult> Block(string userId)
        {
            var response = await _adminService.GetUserByIdAsync(userId);

            if (response != null && response.IsSuccess)
            {
                UserDto model = JsonConvert.DeserializeObject<UserDto>(Convert.ToString(response.Result));

                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Block([FromForm] string Id, [FromForm] DateTimeOffset dateTimeOffset)
        {
            if (ModelState.IsValid)
            {
                var response = await _adminService.BlockUserByIdAsync(Id, dateTimeOffset);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["error"] = response.DisplayMessage;
            }

            return View();
        }

        public async Task<IActionResult> Delete(string userId)
        {
            var response = await _adminService.DeleteUserAsync(userId);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
