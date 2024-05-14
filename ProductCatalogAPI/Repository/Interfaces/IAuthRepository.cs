using ProductCatalogAPI.Models.Dto;

namespace ProductCatalogAPI.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
