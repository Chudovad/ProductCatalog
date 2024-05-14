using ProductCatalogWeb.Models.Dto;

namespace ProductCatalogWeb.Services.Interfaces
{
    public interface IAdminService
    {
        Task<ResponseDto> GetAllUserAsync();
        Task<ResponseDto> GetUserByIdAsync(string userId);
        Task<ResponseDto> BlockUserByIdAsync(string userId, DateTimeOffset dateTimeOffset);
        Task<ResponseDto> CreateUserAsync(RegistrationRequestDto user);
        Task<ResponseDto> ChangeUserPasswordAsync(string userId, string newPassword);
        Task<ResponseDto> DeleteUserAsync(string userId);
    }
}
