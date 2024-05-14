using ProductCatalogWeb.Models.Dto;
using ProductCatalogWeb.Services.Interfaces;

namespace ProductCatalogWeb.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticData.ProductCatalogAPIBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.POST,
                Data = loginRequestDto,
                Url = StaticData.ProductCatalogAPIBase + "/api/auth/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticData.ProductCatalogAPIBase + "/api/auth/register"
            }, withBearer: false);
        }
    }
}
