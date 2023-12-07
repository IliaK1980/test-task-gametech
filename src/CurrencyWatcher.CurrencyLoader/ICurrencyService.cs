namespace CurrencyWatcher.CurrencyLoader
{
    internal interface ICurrencyService
    {
        Task LoadCurrenciesExchangeRates();
    }
}