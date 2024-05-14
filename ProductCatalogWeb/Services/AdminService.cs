using ProductCatalogWeb.Models.Dto;
using ProductCatalogWeb.Services.Interfaces;
using System;

namespace ProductCatalogWeb.Services
{
    public class AdminService : IAdminService
    {
        private readonly IBaseService _baseService;
        public AdminService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> BlockUserByIdAsync(string userId, DateTimeOffset dateTimeOffset)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.POST,
                Data = dateTimeOffset,
                Url = StaticData.ProductCatalogAPIBase + "/api/users/" + userId + "/block"
            });
        }

        public async Task<ResponseDto> ChangeUserPasswordAsync(string userId, string newPassword)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.PUT,
                Data = newPassword,
                Url = StaticData.ProductCatalogAPIBase + "/api/users/" + userId + "/password"
            });
        }

        public async Task<ResponseDto> CreateUserAsync(RegistrationRequestDto user)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.POST,
                Data = user,
                Url = StaticData.ProductCatalogAPIBase + "/api/users/"
            });
        }

        public async Task<ResponseDto> DeleteUserAsync(string userId)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.DELETE,
                Url = StaticData.ProductCatalogAPIBase + "/api/users/" + userId
            });
        }

        public async Task<ResponseDto> GetAllUserAsync()
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.ProductCatalogAPIBase + "/api/users/"
            });
        }

        public async Task<ResponseDto> GetUserByIdAsync(string userId)
        {
            return await _baseService.SendAsync(new ApiRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.ProductCatalogAPIBase + "/api/users/" + userId
            });
        }
    }
}
