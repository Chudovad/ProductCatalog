using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogAPI.Models.Dto;
using ProductCatalogAPI.Repository.Interfaces;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/categories")]
    public class CategoryAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private readonly ICategoryRepository _repository;

        public CategoryAPIController(ICategoryRepository repository)
        {
            _repository = repository;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<CategoryDto> categoryDtos = await _repository.GetCategories();
                _response.Result = categoryDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                CategoryDto categoryDto = await _repository.GetCategoryById(id);
                _response.Result = categoryDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [Authorize(Roles = $"{StaticData.RoleManager}")]
        [HttpPost]
        public async Task<object> Post([FromBody] CategoryDto categoryDto)
        {
            try
            {
                CategoryDto model = await _repository.CreateUpdateCategory(categoryDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [Authorize(Roles = $"{StaticData.RoleManager}")]
        [HttpPut]
        public async Task<object> Put([FromBody] CategoryDto categoryDto)
        {
            try
            {
                CategoryDto model = await _repository.CreateUpdateCategory(categoryDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [Authorize(Roles = $"{StaticData.RoleManager}")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _repository.DeleteCategory(id);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}
