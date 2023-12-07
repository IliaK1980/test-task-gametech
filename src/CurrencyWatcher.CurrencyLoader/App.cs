namespace CurrencyWatcher.CurrencyLoader
{
    internal class App(ICurrencyService currencyService)
    {
        public async Task Run()
        {
            await currencyService.LoadCurrenciesExchangeRates();
        }
    }
}
