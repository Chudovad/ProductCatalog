using ProductCatalogAPI.Models.Dto;

namespace ProductCatalogAPI.Repository.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<UserDto>> GetAllUserAsync();
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<bool> BlockUserByIdAsync(string userId, DateTimeOffset dateTimeOffset);
        Task<string> CreateUserAsync(RegistrationRequestDto user);
        Task<bool> ChangeUserPasswordAsync(string userId, string newPassword);
        Task<bool> DeleteUserAsync(string userId);
    }
}
