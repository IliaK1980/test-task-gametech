using CurrencyWatcher.Domain.Models;
using Microsoft.Extensions.Options;

namespace CurrencyWatcher.CurrencyLoader
{
    internal class CurrenciesExchangeRatesProvider : ICurrenciesExchangeRatesProvider
    {
        private readonly ICurrenciesExchangeRatesParser _currenciesParser;
        private readonly ExchangeRatesProviderOptions _options;

        public CurrenciesExchangeRatesProvider(ICurrenciesExchangeRatesParser currenciesParser, IOptions<ExchangeRatesProviderOptions> options)
        {
            _currenciesParser = currenciesParser;
            _options = options.Value;
        }

        public async Task<Currency[]> GetCurrenciesExchangeRates()
        {
            using var client = new HttpClient();

            var currentYear = DateTime.Now.Year;
            var url = $"{_options.ExchangeRatesUrl}?year={currentYear}";

            using var currenciesStream = await client.GetStreamAsync(url);

            return _currenciesParser.ParseCurrenciesExchangeRates(currenciesStream);
        }
    }
}
