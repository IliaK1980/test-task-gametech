using CurrencyWatcher.Domain.Models;

namespace CurrencyWatcher.CurrencyLoader
{
    internal interface ICurrenciesExchangeRatesProvider
    {
        Task<Currency[]> GetCurrenciesExchangeRates();
    }
}