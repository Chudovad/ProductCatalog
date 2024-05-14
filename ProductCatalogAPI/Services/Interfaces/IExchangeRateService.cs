using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Services.Interfaces
{
    public interface IExchangeRateService
    {
        Task<decimal> GetUSDRateForToday();
    }
}
