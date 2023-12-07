using CurrencyWatcher.Domain.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyWatcher.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrenciesRepository _currenciesRepository;

        public CurrenciesController(ICurrenciesRepository currenciesRepository)
        {
            _currenciesRepository = currenciesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _currenciesRepository.GetAllCurrenciesAsync());
        }

        [HttpGet("{id}/rate")]
        public async Task<IActionResult> GetCurrencyRate(int id, DateOnly date)
        {
            var currencyRate = await _currenciesRepository.GetCurrencyRateAsync(id, date);

            if (currencyRate == null)
            {
                return NotFound();
            }

            return Ok(currencyRate);
        }
    }
}
