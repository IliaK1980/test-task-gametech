using CurrencyWatcher.Domain.Infrastructure;

namespace CurrencyWatcher.CurrencyLoader
{
    internal class CurrencyService : ICurrencyService
    {
        private readonly ICurrenciesRepository _currenciesRepository;
        private readonly ICurrenciesExchangeRatesProvider _currenciesExchangeRatesProvider;

        public CurrencyService(ICurrenciesRepository currenciesRepository, ICurrenciesExchangeRatesProvider currenciesExchangeRatesProvider)
        {
            _currenciesRepository = currenciesRepository;
            _currenciesExchangeRatesProvider = currenciesExchangeRatesProvider;
        }

        public async Task LoadCurrenciesExchangeRates()
        {
            var currencies = await _currenciesExchangeRatesProvider.GetCurrenciesExchangeRates();
            await _currenciesRepository.SaveCurrenciesAsync(currencies);
        }
    }
}
