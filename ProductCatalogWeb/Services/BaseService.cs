using Newtonsoft.Json;
using ProductCatalogWeb.Models.Dto;
using ProductCatalogWeb.Services.Interfaces;
using static ProductCatalogWeb.StaticData;
using System.Net.Mime;
using System.Net;
using System.Text;

namespace ProductCatalogWeb.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto> SendAsync(ApiRequest apiRequest, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("ProductCatalog");
                HttpRequestMessage message = new();

                message.Headers.Add("Accept", "application/json");

                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage apiResponse = null;

                switch (apiRequest.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, DisplayMessage = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, DisplayMessage = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, DisplayMessage = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, DisplayMessage = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = ex.Message.ToString(),
                    IsSuccess = false,
                    ErrorMessages = new List<string> { ex.ToString() }
                };
                return dto;
            }
        }
    }
}
