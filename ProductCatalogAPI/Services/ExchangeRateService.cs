using Newtonsoft.Json;
using ProductCatalogAPI.Models;
using ProductCatalogAPI.Services.Interfaces;

namespace ProductCatalogAPI.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        public async Task<decimal> GetUSDRateForToday()
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://api.nbrb.by/exrates/rates/USD?parammode=2");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usdRate = JsonConvert.DeserializeObject<ExchangeRate>(content);
                return usdRate.Cur_OfficialRate;
            }
            return 0;
        }
    }
}
