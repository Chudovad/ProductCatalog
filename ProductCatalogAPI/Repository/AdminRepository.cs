using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Models;
using ProductCatalogAPI.Models.Dto;
using ProductCatalogAPI.Repository.Interfaces;

namespace ProductCatalogAPI.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AuthDbContext _context;
        private readonly IAuthRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminRepository(AuthDbContext context, UserManager<ApplicationUser> userManager, IAuthRepository repository) 
        {
            _context = context;
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<bool> BlockUserByIdAsync(string userId, DateTimeOffset dateTimeOffset)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.SetLockoutEndDateAsync(user, dateTimeOffset);
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeUserPasswordAsync(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<string> CreateUserAsync(RegistrationRequestDto registrationRequestDto)
        {
            var result = await _repository.Register(registrationRequestDto);
            await _repository.AssignRole(registrationRequestDto.Email, registrationRequestDto.Role);

            return result;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var user = await _context.ApplicationUsers.ToListAsync();
            
            return user.Select(i => new UserDto 
            { 
                ID = i.Id, 
                Email = i.Email, 
                Name = i.Name, 
                PhoneNumber = i.PhoneNumber, 
                Role = _userManager.GetRolesAsync(i).GetAwaiter().GetResult().FirstOrDefault() 
            });
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(i => i.Id == userId);
            var role = await _context.Roles.FirstOrDefaultAsync(i => i.Id == _context.UserRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId);

            UserDto userDto = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Role = role.Name
            };

            return userDto;
        }
    }
}
