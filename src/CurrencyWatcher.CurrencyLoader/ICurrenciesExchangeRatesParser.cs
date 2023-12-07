using CurrencyWatcher.Domain.Models;

namespace CurrencyWatcher.CurrencyLoader
{
    internal interface ICurrenciesExchangeRatesParser
    {
        Currency[] ParseCurrenciesExchangeRates(Stream currenciesStream);
    }
}