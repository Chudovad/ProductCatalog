using ProductCatalogWeb.Models.Dto;

namespace ProductCatalogWeb.Services.Interfaces
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(ApiRequest apiRequest, bool withBearer = true);
    }
}
