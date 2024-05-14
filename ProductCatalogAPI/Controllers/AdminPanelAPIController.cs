using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogAPI.Models.Dto;
using ProductCatalogAPI.Repository.Interfaces;

namespace ProductCatalogAPI.Controllers
{
    [Authorize(Roles = $"{StaticData.RoleAdmin}")]
    [Route("api/users")]
    public class AdminPanelAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IAdminRepository _repository;

        public AdminPanelAPIController(IAdminRepository repository)
        {
            _repository = repository;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<UserDto> users = await _repository.GetAllUserAsync();
                _response.Result = users;
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
        public async Task<object> Get(string id)
        {
            try
            {
                UserDto user = await _repository.GetUserByIdAsync(id);
                _response.Result = user;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<object> Post([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            try
            {
                var model = await _repository.CreateUserAsync(registrationRequestDto);
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

        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(string id)
        {
            try
            {
                bool isSuccess = await _repository.DeleteUserAsync(id);
                _response.IsSuccess = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{userId}/password")]
        public async Task<object> Put(string userId, [FromBody] string newPassword)
        {
            try
            {
                bool isSuccess = await _repository.ChangeUserPasswordAsync(userId, newPassword);
                _response.IsSuccess = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("{id}/block")]
        public async Task<object> Post(string id, [FromBody] DateTimeOffset dateTimeOffset)
        {
            try
            {
                bool isSuccess = await _repository.BlockUserByIdAsync(id, dateTimeOffset);
                _response.IsSuccess = isSuccess;
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
