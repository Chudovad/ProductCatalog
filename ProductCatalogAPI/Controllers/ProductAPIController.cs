using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogAPI.Models.Dto;
using ProductCatalogAPI.Repository.Interfaces;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private readonly IProductRepository _repository;

        public ProductAPIController(IProductRepository repository)
        {
            _repository = repository;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await _repository.GetProducts();
                _response.Result = productDtos;
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
        [Route("search")]
        public async Task<object> Get(string searchText)
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await _repository.SearchProduct(searchText);
                _response.Result = productDtos;
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
        [Route("filter")]
        public async Task<object> Get(int priceStart, int priceEnd)
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await _repository.FilterProductByPrice(priceStart, priceEnd);
                _response.Result = productDtos;
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
                ProductDto productDto = await _repository.GetProductById(id);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [Authorize(Roles = $"{StaticData.RoleUser},{StaticData.RoleManager}")]
        [HttpPost]
        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _repository.CreateUpdateProduct(productDto);
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

        [Authorize(Roles = $"{StaticData.RoleUser},{StaticData.RoleManager}")]
        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await _repository.CreateUpdateProduct(productDto);
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
                bool isSuccess = await _repository.DeleteProduct(id);
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
