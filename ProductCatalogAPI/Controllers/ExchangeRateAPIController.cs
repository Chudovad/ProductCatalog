using Microsoft.AspNetCore.Mvc;
using ProductCatalogAPI.Models.Dto;
using ProductCatalogAPI.Services.Interfaces;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/rate")]
    public class ExchangeRateAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private readonly IExchangeRateService _rateService;

        public ExchangeRateAPIController(IExchangeRateService rateService)
        {
            _response = new ResponseDto();
            _rateService = rateService;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                decimal curRate = await _rateService.GetUSDRateForToday();
                _response.Result = curRate;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = ex.Message;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}
