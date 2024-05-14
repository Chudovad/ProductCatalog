using ProductCatalogWeb.Models.Dto;

namespace ProductCatalogWeb.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationData);
        Task<ResponseDto> LoginAsync(LoginRequestDto loginData);
        Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto roleData);
    }
}
